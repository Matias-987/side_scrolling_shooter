using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Option : MonoBehaviour
{
    public Controller_Player parent;

    private Vector3 offset;

    private GameObject laser;

    private float missileTimer = 0f;
    public float missileCooldown = 4f;

    void Start()
    {
        missileTimer = missileCooldown;  // Inicializa listo para disparar el misil
        Controller_Player._Player.OnShooting += Shoot;
        parent = Controller_Player._Player;
        offset = new Vector3(parent.transform.position.x - this.transform.position.x, parent.transform.position.y - this.transform.position.y, parent.transform.position.z - this.transform.position.z);
    }

    

    private void FixedUpdate()
    {
        // Mantiene una posicion relativa al jugador
        if(parent!=null)
        transform.position = parent.transform.position - offset;
    }

    
    private void Update()
    {
        if (missileTimer > 0)
        {
            missileTimer -= Time.deltaTime;
        }
        if (parent == null)
            Destroy(this.gameObject);

        if (Input.GetKey(KeyCode.O))
        {
            if (laser != null)
            {
                laser.GetComponent<Controller_Laser>().relase = false;
            }
        }
        else
        {
            if (laser != null)
            {
                laser.GetComponent<Controller_Laser>().relase = true;
                laser = null;
            }
        }
    }

    public void Shoot()
    {
        // Disparar láser si está activo
        if (parent.laserOn)
        {
            laser = Instantiate(parent.laserProjectile, transform.position, Quaternion.identity);
            laser.GetComponent<Controller_Laser>().parent = this.gameObject;
        }

        // Disparar proyectil base solo si el láser NO está activo
        if (!parent.laserOn)
        {
            Instantiate(parent.projectile, transform.position, Quaternion.identity);
        }

        // Aplicar doble disparo (independiente del láser)
        if (parent.doubleShoot)
        {
            // Crear dos direcciones alternadas para el doble disparo
            Vector3 offsetUp = transform.position + new Vector3(0, 0.5f, 0);
            Vector3 offsetDown = transform.position + new Vector3(0, -0.5f, 0);

            GameObject doubleProjUp = Instantiate(parent.doubleProjectile, offsetUp, Quaternion.identity);
            GameObject doubleProjDown = Instantiate(parent.doubleProjectile, offsetDown, Quaternion.identity);

            // Configurar direcciones alternas
            doubleProjUp.GetComponent<Controller_Projectile_Double>().directionUp = true;
            doubleProjDown.GetComponent<Controller_Projectile_Double>().directionUp = false;
        }

        if (parent.missiles && missileTimer <= 0)
        {
            GameObject missile = Instantiate(parent.missileProjectile, transform.position, Quaternion.Euler(0, 0, 90));
            missile.GetComponent<Controller_Missile>();
            missileTimer = parent.missileCooldown;
        }
    }
}
