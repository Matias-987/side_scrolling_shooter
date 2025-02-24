using UnityEngine;

public class Controller_Missile : Projectile
{
    public float homingForce = 5f;
    public float explosionRadius = 5f;
    public float explosionForce = 500f;
    public GameObject explosionEffect;

    private Transform target;
    private Rigidbody rb;
    private bool impactado = false; // Evita múltiples explosiones
    private bool yaExplotado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        FindClosestEnemy();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.AddForce(direction * homingForce);
        }
    }

    internal override void OnCollisionEnter(Collision collision)
    {
        if (!impactado)
        {
            // Detona al chocar con capas específicas (ajusta según tu juego)
            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Enemy"))
            {
                impactado = true;
                Explotar();
                Destroy(gameObject);
            }
        }
    }

    void Explotar()
    {
        if (!yaExplotado)
        {
            yaExplotado = true;

            // Efecto de explosión
            if (explosionEffect != null)
            {
                GameObject particulas = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(particulas, 2f);
            }

            // Detecta enemigos en el radio
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    // Llama a la lógica de muerte del enemigo (generar power-up y puntos)
                    Controller_Enemy enemyScript = enemy.GetComponent<Controller_Enemy>();
                    if (enemyScript != null)
                    {
                        enemyScript.GeneratePowerUp();
                        Controller_Hud.points++;
                    }
                    Destroy(enemy.gameObject); // Destruye al enemigo
                }

                // Fuerza física en el área (opcional)
                Rigidbody rb = enemy.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy.transform;
            }
        }
    }
}
