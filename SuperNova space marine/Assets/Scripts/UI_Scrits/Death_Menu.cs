using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_Menu : MonoBehaviour
{
    public GameObject deathUI;
    public Player_Health playerHP;
    bool alive = true;
    void Update()
    {
        if (playerHP.health <= 0 && alive == true)
        {
            DeathScreen();
            alive = false;  
            Time.timeScale = 0;
        }
    }

   public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   void DeathScreen()
    {
        deathUI.SetActive(true);
    }
}
