using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour
{
    #region variables
    Scene loadedScene;
    public Canvas hudCanvas;
    #endregion
    #region Scene Change Detection
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//called when a scene is changed.
    {
        loadedScene = SceneManager.GetActiveScene();//returns the active scene
        if (loadedScene.name == "Main_Menu" || loadedScene.name == "Intro_Movie")//menu or intro movie scenes are active, disable the HUD.
        {
            gameObject.SetActive(false);
        }
        else 
        { 
            gameObject.SetActive(true);
        }
    }
    #endregion

    private void Awake()
    {
        hudCanvas = transform.parent.GetComponent<Canvas>();
    }
    void Start()
    {
        DontDestroyOnLoad(hudCanvas.gameObject);//Makes the canvas of the HUD persist between scenes
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
