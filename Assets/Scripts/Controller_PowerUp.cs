using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_PowerUp : Projectile
{
    private Rigidbody rb;
    private Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = Controller_Player._Player.transform;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(-0.7f, 0, 0);

        if (Controller_Player._Player.magnetActive)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            movement += direction * Controller_Player._Player.magnetForce;
        }
        rb.velocity = movement;
    }
}
