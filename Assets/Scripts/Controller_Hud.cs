using UnityEngine;
using UnityEngine.UI;

public class Controller_Hud : MonoBehaviour
{
    public Text gameOverText, pointsText, powerUpText;
    public static bool gameOver;
    public static int points;
    private Controller_Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Controller_Player>();
        gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverText.gameObject.SetActive(true);
        }
        pointsText.text = $"Puntos: {points}";
        powerUpText.text = player.magnetActive ? "Iman activo!" : $"Power-Ups: {player.powerUpCount}";
    }

    private string GetPowerUpStatus()
    {
        return player.powerUpCount switch
        {
            1 => "PowerUp: Speed Boost",
            2 => "PowerUp: Missiles",
            3 => "PowerUp: Double Shot",
            4 => "PowerUp: Laser",
            5 => "PowerUp: Options",
            >= 6 => "PowerUp: Force Field",
            _ => "PowerUp: None"
        };
    }
}
