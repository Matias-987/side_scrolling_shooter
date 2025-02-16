using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float xLimit = 30, yLimit = 20;

    protected virtual void Update() => CheckLimits();

    // Método virtual para límites
    protected virtual void CheckLimits()
    {
        if (transform.position.x > xLimit || transform.position.x < -xLimit || transform.position.y > yLimit || transform.position.y < -yLimit)
        {
            Destroy(gameObject);
        }
    }

    // Método virtual para colisiones (requerido para el error CS0115)
    protected virtual void OnCollisionEnter(Collision collision)
    {
        // Comportamiento base: Destruir al chocar con paredes/suelo
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
