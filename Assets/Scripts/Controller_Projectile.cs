using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Projectile : Projectile
{
    public float projectileSpeed;

    public Rigidbody rb;

    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    
    public override void Update()
    {
        ProjectileDirection();
        base.Update();
    }

    // Direccion del proyectil
    public virtual void ProjectileDirection()
    {
        rb.velocity = new Vector3(1 * projectileSpeed, rb.velocity.y, 0);
    }

    internal override void CheckLimits()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        // Destruir solo cuando salga completamente de la vista con 10% de margen
        if (viewportPos.x < -0.1f || viewportPos.x > 1.1f || viewportPos.y < -0.1f || viewportPos.y > 1.1f)
        {
            Destroy(gameObject);
        }
    }
}
