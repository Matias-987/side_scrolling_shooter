using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_EnemyProjectile : Projectile
{
    // Configuración del proyectil enemigo
    private GameObject player;
    private Vector3 direction;
    private Rigidbody rb;
    public float enemyProjectileSpeed; 

    void Start()
    {
        // Calcula la direccion hacia el jugador
        if (Controller_Player._Player != null)
        {
            player = Controller_Player._Player.gameObject;
            direction = -(this.transform.localPosition - player.transform.localPosition).normalized;
        }
        rb = GetComponent<Rigidbody>();
    }

    
    public override void Update()
    {
        // Aplica la fuerza de disparo en la direccion calculada
        rb.AddForce(direction*enemyProjectileSpeed);
        base.Update();
    }
}
