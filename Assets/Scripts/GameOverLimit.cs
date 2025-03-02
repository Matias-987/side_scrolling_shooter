using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverLimit : MonoBehaviour
{
    public static int enemiesEscaped = 0; // Contador estático de enemigos escapados
    public Text escapedCounterText;

    // Resetear el contador al iniciar
    private void Start()
    {
        enemiesEscaped = 0;
        UpdateEscapedCounter();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destruir proyectiles aliados y enemigos
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
        // Contar enemigos que llegan al límite
        Debug.Log("Colisiona el enemigo");
        if (other.CompareTag("Enemy"))
        {
            enemiesEscaped++; // Incrementar contador
            UpdateEscapedCounter();
            Destroy(other.gameObject); // Destruir enemigo

            Debug.Log($"Enemigos escapados: {enemiesEscaped}"); // Log para depuración

            if (enemiesEscaped >= 10)
            {
                Controller_Hud.gameOver = true;
            }
        }
    }

    private void UpdateEscapedCounter()
    {
        if (escapedCounterText != null)
        {
            escapedCounterText.text = $"Escaped: {enemiesEscaped}/10";
        }
    }

    public static void ResetCounter()
    {
        enemiesEscaped = 0;
    }
}
