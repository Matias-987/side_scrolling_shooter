using UnityEngine;

public class Controller_Missile : Projectile
{
    public Vector3 normalForce = new Vector3(30, -30, 0);
    public Vector3 wallForce = new Vector3(0, 50, 0);
    private Rigidbody rb;
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
        Physics.IgnoreLayerCollision(9, 9); // Ignorar colisiones entre proyectiles
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.right, out RaycastHit hit, 3))
        {
            transform.rotation = Quaternion.Euler(0, 0, hit.collider.transform.rotation.z - initialRotation.z);
        }
        else
        {
            transform.rotation = initialRotation;
        }
    }
}
