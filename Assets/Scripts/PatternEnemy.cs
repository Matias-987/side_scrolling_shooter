using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternEnemy : Controller_Enemy
{
    public bool goingUp;

    public bool forward;

    private float timer=1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    override public void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            // Alterna entre movimiento frontal y diagonal
            rb.velocity = Vector3.zero;
            if (forward)
            {
                forward = false;
            }
            else
            {
                goingUp = !goingUp;  // Cambia la direccion vertical
                forward = true;
            }
            timer = 1f;
        }
        base.Update();  // Ejecuta la logica base del enemigo
    }

    void FixedUpdate()
    {
        if (forward)
        {
            // Movimiento recto hacia la izquierda
            rb.AddForce(new Vector3(-1, 0, 0) * enemySpeed,ForceMode.Impulse);
        }
        else
        {
            // Movimiento diagonal seun la direccion vertical
            if (goingUp)
            {
                rb.AddForce(new Vector3(-1, -1, 0) * enemySpeed, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(new Vector3(-1, 1, 0) * enemySpeed, ForceMode.Impulse);
            }
        }

    }
}
