using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigzagEnemy : Controller_Enemy
{
    public bool goingUp;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Aplica la fuerza segun la direccion actual
        if (goingUp)
        {
            rb.AddForce(new Vector3(-1, 1, 0) * enemySpeed);
        }
        else
        {
            rb.AddForce(new Vector3(-1, -1, 0) * enemySpeed);
        }
    }

    internal override void OnCollisionEnter(Collision collision)
    {
        // Cambia de dirreccion al colisionar con objetos
        if (collision.gameObject.CompareTag("Floor"))
        {
            goingUp = true;  // Rebote hacia arriba
        }
        if (collision.gameObject.CompareTag("Ceiling"))
        {
            goingUp = false;  // Rebote hacia abajo
        }
        base.OnCollisionEnter(collision);  // Ejecuta la logica base de la colision
    }
}
