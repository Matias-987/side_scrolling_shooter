using UnityEngine;
using System.Collections.Generic;

public class Controller_Enemy : MonoBehaviour
{
    public float enemySpeed;
    public float xLimit;
    public List<GameObject> powerUpPrefabs;
    public GameObject enemyProjectile;
    protected Rigidbody rb;
    private float shootingCooldown;

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        shootingCooldown = Random.Range(1, 10);
    }

    void Update()
    {
        shootingCooldown -= Time.deltaTime;
        CheckLimits();
        if (shootingCooldown <= 0) ShootPlayer();
    }

    private void ShootPlayer()
    {
        if (shootingCooldown <= 0 && Controller_Player._Player != null)
        {
            Instantiate(enemyProjectile, transform.position, Quaternion.identity);
            shootingCooldown = Random.Range(1, 10);
        }
    }

    private void CheckLimits()
    {
        if (transform.position.x < xLimit) Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("Laser"))
        {
            GeneratePowerUp();
            Destroy(gameObject);
            Controller_Hud.points++;
        }
    }

    private void GeneratePowerUp()
    {
        if (Random.Range(0, 3) == 2) // 33% de probabilidad
        {
            Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)], transform.position, Quaternion.identity);
        }
    }
}
