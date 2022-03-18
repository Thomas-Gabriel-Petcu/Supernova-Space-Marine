using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSkip : MonoBehaviour
{
   public GameObject loader;
    public float timer;
    
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            loader.GetComponent<levelLoader>().SceneLoad(3);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            loader.GetComponent<levelLoader>().SceneLoad(3);
        }
    }
}