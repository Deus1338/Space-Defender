using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   [Header("Enemy Stats")]
    [SerializeField] int health = 100;
    [SerializeField] int pointsPerKill = 100;

    [Header("Shooting Params")]
    float shotCounter;
    [SerializeField] float minShotDelay = 0.2f;
    [SerializeField] float maxShotDelay = 3f;
    [SerializeField] GameObject laserShot;
    [SerializeField] float laserSpeed = -10f;

    [Header("Destroy Effects")]
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float destroyEffectTime = 0.1f;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.1f;
    

    void Start()
    {
        shotCounter = Random.Range(minShotDelay, maxShotDelay);
    }

    // Update is called once per frame
    void Update()
    {
        CountingDownBeforeFire();

    }

    private void CountingDownBeforeFire()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minShotDelay, maxShotDelay);
        }
    }

    private void Fire()
    {
        //Vector2 laserAppearPosition = new Vector2(transform.position.x, transform.position.y - 0.7f);
        GameObject shot = Instantiate(laserShot, transform.position, Quaternion.identity) as GameObject;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
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
        FindObjectOfType<Score>().AddScore(pointsPerKill);
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject);
        var expEffect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(expEffect, destroyEffectTime);
    }
}
