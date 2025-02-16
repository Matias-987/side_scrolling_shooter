using UnityEngine;

public class Controller_Magnet : BasePowerUp
{
    [Header("Configuraci�n Iman")]
    public float magnetDuration = 10f;
    public float magnetStrength = 5f;

    public override void ApplyEffect(Controller_Player player)
    {
        player.ActivateMagnet(magnetDuration, magnetStrength);
        DestroyAfterCollection(); // Usa el m�todo de la clase base
    }
}
