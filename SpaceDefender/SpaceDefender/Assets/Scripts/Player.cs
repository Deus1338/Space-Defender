using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float distanceFromBorder = 0.5f;
    public int health = 500;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float destroyEffectTime = 0.1f;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.1f;

    [Header("Player Shooting")]
    [SerializeField] GameObject laserShot;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float firePeriod = 1f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Start()
    {
        DefineStartPosition();
        SetUpMoveBoundaries();
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {    
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContiniously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContiniously()
    {
        while (true)
        {
            //Vector2 laserAppearPosition = new Vector2(transform.position.x, transform.position.y + 0.6f);
            GameObject laser = Instantiate(laserShot, transform.position,
                                                                   Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(firePeriod);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + distanceFromBorder;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - distanceFromBorder;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + distanceFromBorder;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - distanceFromBorder;
    }

    private void DefineStartPosition()
    {
        transform.position = new Vector2(0, -6);
    }

    public int GetHealth()
    {
        return health;
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        FindObjectOfType<Level>().LoadGameOverScene();
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject);
        var expEffect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(expEffect, destroyEffectTime);
    }
}
