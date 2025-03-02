using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_PowerUp : Projectile
{
    private Rigidbody rb;
    private bool isAttracted;

    public float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.left * moveSpeed;
    }

    void FixedUpdate()
    {
        // Atracción hacia el jugador si el imán está activo
        if (isAttracted && Controller_Player.Instance.magnetActive)
        {
            Vector3 direction = (Controller_Player.Instance.transform.position - transform.position).normalized;
            rb.velocity = (Vector3.left * moveSpeed) + (direction * 15f);  // Velocidad aumentaa por el iman
        }
        else
        {
            rb.velocity = Vector3.left * moveSpeed;  // Mantener velocidad base
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si entra en el área del imán y este está activo
        if (other.CompareTag("Magnet") && Controller_Player.Instance.magnetActive)
        {
            isAttracted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si sale del área del imán
        if (other.CompareTag("Magnet"))
        {
            isAttracted = false;
        }
    }
}
