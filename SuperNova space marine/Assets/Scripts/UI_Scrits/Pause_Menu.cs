using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{ //every time we change the scene we must grab a new reference for pauseMenuUI and optionsMenuUI
    public static bool gameIsPaused = false;
    private GameObject optionsMenuUI;
    private GameObject pauseMenuUI;

    void Awake()
    {
        pauseMenuUI = GameObject.FindGameObjectWithTag("pauseMenuUI");
        optionsMenuUI = GameObject.FindGameObjectWithTag("optionsMenuUI");
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        if (optionsMenuUI!= null)
        {
            optionsMenuUI.SetActive(false);
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }


        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Transform pauseMenuHolder = pauseMenuUI.transform.GetChild(0);
        pauseMenuHolder.gameObject.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }
   
    public void Quit()
    {
        Application.Quit();
    }


}
