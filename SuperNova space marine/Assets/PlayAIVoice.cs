using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAIVoice : MonoBehaviour
{
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SFX_Manager.instance.Play("Health_Critical");
        }
    }
}
