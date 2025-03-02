using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLimit : MonoBehaviour
{
    [SerializeField] private List<Controller_Enemy> enemiesInZone = new List<Controller_Enemy>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Controller_Enemy enemy = other.GetComponent<Controller_Enemy>();
            if (enemy != null && !enemiesInZone.Contains(enemy))
            {
                enemiesInZone.Add(enemy);
                enemy.ToggleShooting(false); // Desactiva el disparo
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Controller_Enemy enemy = other.GetComponent<Controller_Enemy>();
            if (enemy != null && enemiesInZone.Contains(enemy))
            {
                enemiesInZone.Remove(enemy);
                enemy.ToggleShooting(true); // Reactiva el disparo
            }
        }
    }

    private void OnDisable()
    {
        // Asegurar que se restaure el disparo al destruir el área
        foreach (Controller_Enemy enemy in enemiesInZone)
        {
            if (enemy != null)
            {
                enemy.ToggleShooting(true);
            }
        }
        enemiesInZone.Clear();
    }
}
