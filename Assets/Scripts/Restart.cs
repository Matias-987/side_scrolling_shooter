using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        // Reinicia el juego al presionar R
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Limpia la instancia del jugador
            if (Controller_Player._Player != null)
            {
                Destroy(Controller_Player._Player.gameObject);
                Controller_Player._Player = null;
            }

            // Reestablece el tiempo y la puntuacion
            Time.timeScale = 1;
            Controller_Hud.gameOver = false;
            Controller_Hud.points = 0;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Recarga la escena actual
        }
    }
}
