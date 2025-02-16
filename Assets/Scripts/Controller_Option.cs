using UnityEngine;

public class Controller_Option : MonoBehaviour
{
    public Controller_Player parent;
    private Vector3 offset;
    private GameObject laser;

    void Start()
    {
        parent = Controller_Player._Player;
        offset = parent.transform.position - transform.position;
        parent.OnShooting += Shoot;
    }

    void FixedUpdate() => transform.position = parent.transform.position - offset;

    void Shoot()
    {
        if (parent.laserOn)
        {
            if (laser == null)
            {
                laser = Instantiate(parent.laserProjectile, transform.position, Quaternion.identity);
                laser.GetComponent<Controller_Laser>().Initialize(gameObject);
            }
        }
        else
        {
            Instantiate(parent.projectile, transform.position, Quaternion.identity);

            if (parent.doubleShoot)
            {
                var doubleProj = Instantiate(parent.doubleProjectile, transform.position, Quaternion.identity);
                doubleProj.GetComponent<Controller_Projectile_Double>().directionUp = Controller_Player.lastKeyUp;
            }
        }
    }

    void OnDisable()
    {
        if (parent != null) parent.OnShooting -= Shoot;
    }
}
