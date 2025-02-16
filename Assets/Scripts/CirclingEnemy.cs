using UnityEngine;

public class CirclingEnemy : Controller_Enemy
{
    [Header("Movimiento Circular")]
    public float circleRadius = 3f;
    public float circleSpeed = 2f;

    private Vector3 _circleCenter;
    private float _angle;
    private bool _isCircling;

    void Start()
    {
        _circleCenter = transform.position + Vector3.right * 2f;
    }

    void FixedUpdate()
    {
        if (_isCircling)
        {
            _angle += circleSpeed * Time.deltaTime;
            Vector3 offset = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0) * circleRadius;
            transform.position = _circleCenter + offset;
        }
        else
        {
            rb.AddForce(Vector3.left * enemySpeed);
            if (Vector3.Distance(_circleCenter, transform.position) > 10f) _isCircling = true;
        }
    }
}
