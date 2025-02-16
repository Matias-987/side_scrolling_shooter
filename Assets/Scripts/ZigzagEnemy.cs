using UnityEngine;

public class ZigzagEnemy : Controller_Enemy
{
    public bool goingUp;

    void Start() => rb = GetComponent<Rigidbody>();

    void FixedUpdate()
    {
        Vector3 dir = goingUp ? new Vector3(-1, 1, 0) : new Vector3(-1, -1, 0);
        rb.AddForce(dir * enemySpeed);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) goingUp = true;
        if (collision.gameObject.CompareTag("Ceiling")) goingUp = false;
        base.OnCollisionEnter(collision);
    }
}
