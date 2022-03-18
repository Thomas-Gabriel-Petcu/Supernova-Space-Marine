using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    static Game_Manager instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("There is more than 1 game manager");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    bool pressed = false;

    private void Update()
    {
        if (pressed == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameObject obj = new GameObject();
                obj.name = "Test For Ability";
            }
            pressed = true;
        }
    }
}
