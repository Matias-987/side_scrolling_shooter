using UnityEngine;

public class Controller_Projectile_Double : Controller_Projectile
{
    public bool directionUp;

    public override void ProjectileDirection()
    {
        Vector3 dir = directionUp ? new Vector3(1, 1, 0) : new Vector3(1, -1, 0);
        rb.velocity = dir * projectileSpeed;
        transform.rotation = Quaternion.Euler(0, 0, directionUp ? 50 : -50);
    }
}
