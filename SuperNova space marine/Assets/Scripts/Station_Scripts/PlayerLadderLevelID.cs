using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderLevelID : MonoBehaviour
{
    public int heightLevel = 0;
    public SpriteRenderer sr;
    GameObject[] goArrayLVL0;
    GameObject[] goArrayLVL1;
    GameObject[] goArrayLVL2;
   public bool checking = false;

    void Start()
    {
        goArrayLVL0 = GameObject.FindGameObjectsWithTag("LVL0");
        goArrayLVL1 = GameObject.FindGameObjectsWithTag("LVL1");
        goArrayLVL2 = GameObject.FindGameObjectsWithTag("LVL2");
        foreach (GameObject obj in goArrayLVL0)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in goArrayLVL1)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in goArrayLVL2)
        {
            obj.SetActive(false);
        }
    }

    void Update()
    {
        if (sr.sortingLayerName == "Player" && checking == false)
        {
            heightLevel = 0;
            checking = true;
        }
        else if (sr.sortingLayerName == "Default" && checking == false)
        {
            heightLevel = 1;
            checking = true;
        }

        if (heightLevel == 0 && checking == true)
        {
            foreach (GameObject obj in goArrayLVL0)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in goArrayLVL1)
            {
                obj.SetActive(false);
            }
            checking = false;

        }
        else if (heightLevel == 1 && checking == true)
        {
            foreach (GameObject obj in goArrayLVL1)
            {
                obj.SetActive(true);

            }
            foreach (GameObject obj in goArrayLVL0)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in goArrayLVL2)
            {
                obj.SetActive(false);
            }
            checking = false;
        }
        else if (heightLevel == 2 && checking == true)
        {
            foreach (GameObject obj in goArrayLVL2)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in goArrayLVL1)
            {
                obj.SetActive(false);
            }

            checking = false;
        }
    }

    
}