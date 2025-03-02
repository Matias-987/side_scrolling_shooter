using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Controller_Enemy : MonoBehaviour
{
    // Configuración base del enemigo
    public float enemySpeed;
    public float xLimit;           // Límite en X para destrucción

    // Configuración de disparo
    private float shootingCooldown; 
    public GameObject enemyProjectile;
    public GameObject powerUp; 
    public float minShootCooldown = 5f;
    public float maxShootCooldown = 10f;
    private bool canShoot = true;

    private List <GameObject> observers = new List<GameObject>();

    void Start()
    {
        shootingCooldown = UnityEngine.Random.Range(minShootCooldown, maxShootCooldown);
    }

    public virtual void Update()
    {
        // Actualiza el temporizador y los comportamientos basicos
        shootingCooldown -= Time.deltaTime;
        ShootPlayer();
    }

    private void ShootPlayer()
    {
        if (!canShoot) return;

        if (Controller_Player._Player != null && shootingCooldown <= 0)
        {
            Instantiate(enemyProjectile, transform.position, Quaternion.identity);
            shootingCooldown = UnityEngine.Random.Range(minShootCooldown, maxShootCooldown);
        }
    }

    internal virtual void OnCollisionEnter(Collision collision)
    {
        // Maneja la colision con los proyectiles del jugador
        if (collision.gameObject.CompareTag("Projectile"))
        {
            NotifyObservers();
            GeneratePowerUp();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            Controller_Hud.points++;
        }
        if (collision.gameObject.CompareTag("Laser"))
        {
            NotifyObservers();
            GeneratePowerUp();
            Destroy(this.gameObject);
            Controller_Hud.points++;
        }
    }

    public void GeneratePowerUp()
    {
        // Genera power ups con probabilidades de 1/3
        int rnd = UnityEngine.Random.Range(0, 3);
        if (rnd == 2)
        {
            Instantiate(powerUp, transform.position, Quaternion.identity);
        }
    }

    public void ToggleShooting(bool enable)
    {
        canShoot = enable;
    }

    public void AddObservers(GameObject missile)
    {
        observers.Add(missile);
    }

    public void RemoveObservers(GameObject missile)
    {
        if (observers.Contains(missile))
        {
            observers.Remove(missile);
        }
    }

    public void NotifyObservers()
    {
        List<GameObject> observers2 = new List<GameObject>(observers);
        foreach(var observer in observers2)
        {
            observer.GetComponent<Controller_Missile>().OnNotifyObservers();
        }
        observers.Clear();
    }
}
