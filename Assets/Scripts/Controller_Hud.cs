using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Controller_Hud : MonoBehaviour
{
    public Text gameOverText;
    public static bool gameOver = false;
    public static int points;
    public static int enemies;
    public Text pointsText;
    public Text powerUpText;
    public Text magnetTimer;
    private Controller_Player player;
    public Text winText;
    public static bool gameWon = false;
    public Text escapedCounterText;

    void Start()
    {
        gameOver = false;
        gameOverText.gameObject.SetActive(false);
        points = 0;
        enemies = 0;
        player = GameObject.Find("Player").GetComponent<Controller_Player>();
        magnetTimer.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Actualizar ambos contadores
        pointsText.text = "Score: " + points.ToString();
        escapedCounterText.text = $"Escaped: {GameOverLimit.enemiesEscaped}/10";

        if (GameOverLimit.enemiesEscaped >= 10)
        {
            gameOverText.text = "GAME OVER\nToo many enemies escaped!";
            gameOverText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        if (!gameWon && !Controller_Hud.gameOver)
        {
            pointsText.text = "Score: " + points.ToString();
            

            if(enemies >= 10)
            {
                gameOver = true;
            }

            if (points >= 100)
            {
                WinGame();
            }
        }
        // Maneja el estado de game over
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverText.text = "Game Over" ;
            gameOverText.gameObject.SetActive(true);
        }

        // Actualiza la informacion del power up
        if (player!=null)
        {
            if (player.powerUpCount <= 0)
            {
                powerUpText.text = "PowerUp: None";
            }
            else if (player.powerUpCount == 1)
            {
                powerUpText.text = "PowerUp: Speed Up";
            }
            else if (player.powerUpCount == 2)
            {
                powerUpText.text = "PowerUp: Missile";
            }
            else if (player.powerUpCount == 3)
            {
                powerUpText.text = "PowerUp: Double shoot";
            }
            else if (player.powerUpCount == 4)
            {
                powerUpText.text = "PowerUp: Laser";
            }
            else if (player.powerUpCount == 5)
            {
                powerUpText.text = "PowerUp: Second ship";
            }
            else if (player.powerUpCount == 6)
            {
                powerUpText.text = "PowerUp: Shield";
            }
            else if(player.powerUpCount == 7)
            {
                powerUpText.text = "PowerUp: Magnet";
            }
            else if( player.powerUpCount == 8)
            {
                powerUpText.text = "PowerUp: Shootingx2";
            }
        }
        pointsText.text = "Score: " + points.ToString();

        // Control del temporizador del iman
        if (Controller_Player.Instance != null)
        {
            bool magnetActive = Controller_Player.Instance.magnetActive;

            magnetTimer.gameObject.SetActive(magnetActive);  // Activa/desactiva el texto segun el estado del iman

            if (magnetActive)
            {
                // Actualiza el texto con el tiempo restante
                magnetTimer.text = $"Magnet: {Controller_Player.Instance.magnetTimer.ToString("0.0")}";
            }
        }
    }

    void WinGame()
    {
        gameWon = true;
        Time.timeScale = 0;
        winText.gameObject.SetActive(true);
        winText.text = "YOU WIN!";
    }
}
