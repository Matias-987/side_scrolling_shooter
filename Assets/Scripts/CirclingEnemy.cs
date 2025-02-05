using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingEnemy : MonoBehaviour
{
    public float circleRadius = 3f;
    public float angularSpeed = 2f;
    private float angle = 0f;
    private Vector3 centerPosition;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        centerPosition = transform.position + new Vector3(2f, 0, 0);
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.left * enemySpeed);

        angle += angularSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * circleRadius;
        transform.position = centerPosition + offset;
    }

    internal override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("floor") || collision.gameObject.CompareTag("Ceiling"))
        {
            angularSpeed *= -1;
        }
    }
}
