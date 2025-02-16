using UnityEngine;

public class Controller_Projectile : Projectile
{
    public float projectileSpeed;
    protected Rigidbody rb;

    void Start() => rb = GetComponent<Rigidbody>();

    protected override void Update()
    {
        ProjectileDirection();
        base.Update();  // Ejecuta el Update() de Projectile
    }

    public virtual void ProjectileDirection() => rb.velocity = Vector3.right * projectileSpeed;
}
