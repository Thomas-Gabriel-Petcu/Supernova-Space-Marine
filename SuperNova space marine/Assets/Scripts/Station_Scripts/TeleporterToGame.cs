using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterToGame : MonoBehaviour

{

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(1);
        }
    }

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            SceneManager.LoadScene(1);
    //        }
    //    }
    //}
}
