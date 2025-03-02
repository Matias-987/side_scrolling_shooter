using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Projectile_Double : Controller_Projectile
{
    public bool directionUp;

    public override void ProjectileDirection()
    {
            if (directionUp)
            {
                // Disparo hacia arriba
                rb.velocity = new Vector3(1 * projectileSpeed, 1 * projectileSpeed, 0);
                transform.rotation = Quaternion.Euler(0, 0, 50);
            }
            else
            {
                // Disparo hacia abajo
                rb.velocity = new Vector3(1 * projectileSpeed, 1 * -projectileSpeed, 0);
                transform.rotation = Quaternion.Euler(0, 0, -50);
            }
    }

    public override void Update()
    {
        base.Update();  // Mantiene el comportamiento base
    }
}
