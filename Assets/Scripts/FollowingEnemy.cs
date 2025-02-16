using UnityEngine;

public class FollowingEnemy : Controller_Enemy
{
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * enemySpeed);
    }
}
