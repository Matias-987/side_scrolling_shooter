using UnityEngine;

public class Controller_EnemyProjectile : Projectile
{
    public float enemyProjectileSpeed;
    private Vector3 playerDirection;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerDirection = (player.transform.position - transform.position).normalized;
    }

    void FixedUpdate() => rb.AddForce(playerDirection * enemyProjectileSpeed);
}
