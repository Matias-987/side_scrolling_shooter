using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingEnemy : Controller_Enemy
{
    // Configuración de movimiento
    public float enemyDistance = 10f; // Distancia antes de empezar a circular
    public float circleRadius = 3f;   // Radio del círculo
    public float circleDuration = 2f; // Tiempo para completar un círculo
    public float angularSpeed = 2f;   // Velocidad angular

    // Variables de estado
    private float angle = 0f;         // Ángulo actual en el círculo
    private Vector3 startPos;
    private Rigidbody rb;
    private float circleTimer = 0f;   // Temporizador para el tiempo de círculo
    private bool isCircling = false;  // Indica si está en modo de círculo

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

            // Comprueba si se alcanzo la distancia para circular
            if (Vector3.Distance(startPos, transform.position) >= enemyDistance)
            {
                StartCircling();
            }
        }

        else
        {
            // Calculo del movimiento circular
            angle += Time.deltaTime * 360f / circleDuration;
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf .Deg2Rad), 0) * circleRadius;
            rb.velocity = offset * (circleRadius * Mathf.PI * 2 / circleDuration);
            circleTimer += Time.deltaTime;

            // Finaliza el circulo despues del tiempo establecido
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
        rb.velocity = Vector3.zero;  // Detiene el movimiento lineal
    }

    private void StopCircling()
    {
        isCircling = false;
        startPos = transform.position;  // Actualiza la posicion de referencia
    }
}
