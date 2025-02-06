using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_MagnetPowerUp : MonoBehaviour
{
    public float magnetDuration = 10f;
    public float attractionForce = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controller_Player player = other.GetComponent<Controller_Player>();

            if (player != null )
            {
                player.ActivateMagnet (magnetDuration, attractionForce);
                Destroy(gameObject);
            }
        }
    }
}
