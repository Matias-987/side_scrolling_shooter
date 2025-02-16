using UnityEngine;

public class Controller_PowerUp : Projectile
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Lógica de recolección del power-up
            Destroy(gameObject);
        }

        base.OnCollisionEnter(collision); // Llama a la lógica base (destrucción con muros/suelo)
    }
}
