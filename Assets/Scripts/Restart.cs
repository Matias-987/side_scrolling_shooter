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
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Controller_Player._Player != null)
            {
                Destroy(Controller_Player._Player.gameObject);
                Controller_Player._Player = null;
            }

            Time.timeScale = 1;
            Controller_Hud.gameOver = false;
            Controller_Hud.points = 0;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
