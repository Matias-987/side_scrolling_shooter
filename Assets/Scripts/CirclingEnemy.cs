using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingEnemy : MonoBehaviour
{
    public float enemySpeed = 5f;
    public float enemyDistance = 10f;
    public float circleRadius = 3f;
    public float circleDuration = 2f;
    public float angularSpeed = 2f;
    private float angle = 0f;
    public GameObject powerUp;
    private Vector3 startPos;
    private Rigidbody rb;
    private float circleTimer = 0f;
    private bool isCircling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position + new Vector3(2f, 0, 0);
    }

    void FixedUpdate()
    {
        if (!isCircling)
        {
            rb.velocity = Vector3.left * enemySpeed;

            if (Vector3.Distance(startPos, transform.position) >= enemyDistance)
            {
                StartCircling();
            }
        }

        else
        {
            angle += Time.deltaTime * 360f / circleDuration;
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf .Deg2Rad), 0) * circleRadius;
            rb.velocity = offset * (circleRadius * Mathf.PI * 2 / circleDuration);
            circleTimer += Time.deltaTime;

            if (circleTimer >= circleDuration)
            {
                StopCircling();
            }
        }
    }

    private void StartCircling()
    {
        isCircling = true;
        circleTimer = 0f;
        startPos = transform.position;
        rb.velocity = Vector3.zero;
    }

    private void StopCircling()
    {
        isCircling = false;
        startPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("Laser"))
        {
            GeneratePowerUp();
            Destroy(gameObject);
            Controller_Hud.points++;
        }

        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Ceiling"))
        {
            angularSpeed *= -1;
        }
    }

    private void GeneratePowerUp()
    {
        int rnd = Random.Range(0, 3);
        if (rnd == 2)
        {
            Instantiate(powerUp, transform.position, Quaternion.identity);
        }
    }
}
