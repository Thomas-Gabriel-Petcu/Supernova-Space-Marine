using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Fade : MonoBehaviour
{
    SpriteRenderer rend;
    Color color1;
    void Start()
    {
        Time.timeScale = 0;
        rend = gameObject.GetComponent<SpriteRenderer>();
        color1 = rend.color;
        StartCoroutine(Fade(0.0f, 3f));
        

    }

    IEnumerator Fade(float value, float time)
    {
        float alpha = rend.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, value, t));
            rend.color = newColor;
            Debug.Log(alpha);
            yield return null;
        }
        
    }
}
   

