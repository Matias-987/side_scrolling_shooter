using UnityEngine;

public abstract class BasePowerUp : MonoBehaviour
{
    public abstract void ApplyEffect(Controller_Player player);

    // Método común para todos los power-ups
    protected void DestroyAfterCollection()
    {
        Destroy(gameObject);
        Controller_Hud.points += 50; // Puntos extras por recolectar
    }
}
