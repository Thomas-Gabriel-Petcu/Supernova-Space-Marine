using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Counter : MonoBehaviour
{
    public int avgFrameRate;

    public void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        Debug.Log(avgFrameRate);
    }
}
