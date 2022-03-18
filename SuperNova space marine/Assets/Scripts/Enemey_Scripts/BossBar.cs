using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
 
    void Start()
    {
        gameObject.GetComponent<Image>().enabled = false;
    }


}
