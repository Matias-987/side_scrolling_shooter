using UnityEngine;

public class Controller_Missile : Projectile
{
    public float homingForce = 5f;
    public float explosionRadius = 5f;
    public float explosionForce = 500f;
    public GameObject explosionEffect;

    public GameObject target;
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
            Vector3 direction = (target.transform.position - transform.position).normalized;
            rb.AddForce(direction * homingForce);
        }
    }

    // Busca al enemigo mas cercano
    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;

        GameObject aux = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                aux = enemy;
            }
        }
        
        if (aux != null)
        {
            target = aux;
            aux.GetComponent<Controller_Enemy>().AddObservers(gameObject);
        }
        else
        {
            Explotar();
            Destroy(gameObject);
        }
    }

    internal override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (!impactado && collision.gameObject.CompareTag("Enemy"))
        {
            target.GetComponent<Controller_Enemy>().RemoveObservers(gameObject);
            impactado = true;
            Explotar();
            Destroy(gameObject);
        }
    }

    //Logica de la explosion
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

    public void OnNotifyObservers()
    {
        FindClosestEnemy();
    }
}
