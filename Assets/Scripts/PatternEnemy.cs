using UnityEngine;

public class PatternEnemy : Controller_Enemy
{
    public float patternInterval = 2f;
    private float patternTimer;
    private bool movingForward = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        patternTimer = patternInterval;
    }

    void Update()
    {
        patternTimer -= Time.deltaTime;
        if (patternTimer <= 0)
        {
            movingForward = !movingForward;
            patternTimer = patternInterval;
        }
    }

    void FixedUpdate()
    {
        if (movingForward)
        {
            rb.AddForce(Vector3.left * enemySpeed, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(new Vector3(-1, Random.Range(-1f, 1f), 0) * enemySpeed);
        }
    }
}
