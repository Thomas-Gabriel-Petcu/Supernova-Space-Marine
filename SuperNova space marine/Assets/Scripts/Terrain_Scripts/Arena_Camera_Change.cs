using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena_Camera_Change : MonoBehaviour
{
    public float duration;
    public float elapsed = 0.0f;
    public float size1;
    public float size2;

    public bool triggered = false;

    GameObject camera;

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
            Invoke("Disable", 3f);
        }
    }
    void Start()
    {
        camera = Camera.main.gameObject;
        Camera.main.orthographic = true;
    }
    void Update()
    {    
        if (triggered == true)
        {
            elapsed += Time.deltaTime / duration;
            Camera.main.orthographicSize = Mathf.Lerp(size1, size2, elapsed);
            camera.transform.GetChild(0).transform.localScale = new Vector3(Camera.main.orthographicSize / 3.63f, Camera.main.orthographicSize / 3.63f, 1);
            if (elapsed > 1.0f)
            {
                triggered = false;
            }
        }
        
    }

    void Disable()
    {
        enabled = false;
    }
}
