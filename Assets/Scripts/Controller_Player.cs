using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Controller_Player : MonoBehaviour
{
    public float speed = 5;
    public int powerUpCount = 0;
    private Rigidbody rb;

    [Header("Disparo")]
    public GameObject projectile, doubleProjectile, missileProjectile, laserProjectile;
    internal bool doubleShoot, missiles, laserOn;
    internal float missileCount, shootingCount = 0;
    public event Action OnShooting;

    [Header("Power-Ups")]
    internal bool forceField, magnetActive;
    public float magnetDuration = 10f;
    private float magnetTimer;
    public float magnetStrength = 5f;

    [Header("Otros")]
    public GameObject option;
    private Renderer render;
    internal GameObject laser;
    private List<Controller_Option> options;
    public static bool lastKeyUp { get; private set; }
    public static Controller_Player _Player;

    private void Awake()
    {
        if (_Player == null)
        {
            _Player = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
        options = new List<Controller_Option>();
    }

    void Update()
    {
        CheckForceField();
        HandleInputs();
        //UpdateMagnet();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(
            Input.GetAxis("Horizontal") * speed,
            Input.GetAxis("Vertical") * speed,
            0
        );
        lastKeyUp = Input.GetKey(KeyCode.W);
    }

    void LateUpdate()
    {
        if (laser != null && !Input.GetKey(KeyCode.O))
        {
            laser.GetComponent<Controller_Laser>().relase = true;
            laser = null;
        }
    }

    private void CheckForceField() => render.material.color = forceField ? Color.blue : Color.red;

    private void HandleInputs()
    {
        shootingCount -= Time.deltaTime;
        missileCount -= Time.deltaTime;

        if (Input.GetKey(KeyCode.O)) HandleShooting();
        if (Input.GetKeyDown(KeyCode.P)) ActivatePowerUp();
    }

    private void HandleShooting()
    {
        if (shootingCount > 0) return;

        if (laserOn)
        {
            if (laser == null)
            {
                laser = Instantiate(laserProjectile, transform.position, Quaternion.identity);
                laser.GetComponent<Controller_Laser>().parent = gameObject;
            }
            laser.GetComponent<Controller_Laser>().relase = false;
        }
        else
        {
            ShootProjectiles();
        }

        shootingCount = 0.1f;
    }

    private void ShootProjectiles()
    {
        OnShooting?.Invoke();
        Instantiate(projectile, transform.position, Quaternion.identity);
        if (doubleShoot) Instantiate(doubleProjectile, transform.position, Quaternion.identity);
        if (missiles && missileCount <= 0)
        {
            Instantiate(missileProjectile, transform.position, Quaternion.Euler(0, 0, 90));
            missileCount = 2;
        }
    }

    private void ShootLaser()
    {
        if (laser == null)
        {
            laser = Instantiate(laserProjectile, transform.position, Quaternion.identity);
            laser.GetComponent<Controller_Laser>().parent = gameObject;
        }
    }

    private void ActivatePowerUp()
    {
        switch (powerUpCount)
        {
            case 1: StartCoroutine(BoostSpeed(2f, 5f)); break;
            case 2: missiles = true; break;
            case 3: doubleShoot = true; break;
            case 4: laserOn = true; break;
            case 5: SpawnOption(); break;
            case 6: forceField = true; break;
            case 7: ActivateMagnet(magnetDuration, magnetStrength); break;
        }
        powerUpCount = 0;
    }

    private IEnumerator BoostSpeed(float multiplier, float duration)
    {
        speed *= multiplier;
        yield return new WaitForSeconds(duration);
        speed /= multiplier;
    }

    public void ActivateMagnet(float duration, float strength)
    {
        StartCoroutine(MagnetEffect(duration, strength));
    }

    private IEnumerator MagnetEffect(float duration, float strength)
    {
        magnetActive = true;
        float timer = duration;

        while (timer > 0)
        {
            AttractPowerUps(strength); // Método existente modificado
            timer -= Time.deltaTime;
            yield return null;
        }

        magnetActive = false;
    }

    /*private void UpdateMagnet()
    {
        if (!magnetActive) return;

        magnetTimer -= Time.deltaTime;
        if (magnetTimer <= 0) magnetActive = false;
        else AttractPowerUps();
    }*/

    private void AttractPowerUps(float strength) // En Controller_Player
    {
        Collider[] powerUps = Physics.OverlapSphere(transform.position, 10f, LayerMask.GetMask("PowerUp"));
        foreach (Collider p in powerUps)
            p.transform.position = Vector3.MoveTowards(p.transform.position, transform.position, strength * Time.deltaTime);
    }

    private void SpawnOption()
    {
        if (options.Count >= 4) return;
        Vector3[] offsets = { new(-1, -2, 0), new(-1, 2, 0), new(-1.5f, -4, 0), new(-1.5f, 4, 0) };
        GameObject op = Instantiate(option, transform.position + offsets[options.Count], Quaternion.identity);
        options.Add(op.GetComponent<Controller_Option>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyProjectile"))
        {
            if (forceField) Destroy(collision.gameObject);
            else
            {
                gameObject.SetActive(false);
                Controller_Hud.gameOver = true;
            }
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            powerUpCount++;
        }
    }
}
