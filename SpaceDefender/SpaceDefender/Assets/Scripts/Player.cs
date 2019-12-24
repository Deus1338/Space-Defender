using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float distanceFromBorder = 0.5f;
    [SerializeField] GameObject laserShot;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float firePeriod = 1f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    // Start is called before the first frame update
    void Start()
    {
        DefineStartPosition();
        SetUpMoveBoundaries();
    }


    // Update is called once per frame
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

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

}
