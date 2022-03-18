using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBarHandler : MonoBehaviour
{
    public Image bar;
    All_Enemies_Health destroyerHp;
    TerrrainGeneration generation;
    bool ran = false;
    bool show = false;
    void Start()
    {

        bar.enabled = false;
       
        generation = GameObject.Find("MainTerGenerator").GetComponent<TerrrainGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (generation.arenaSpawned == true && ran == false)
        {
            //grabs reference to the destroyer's HP as soon as it is instantiated
            destroyerHp = GameObject.FindGameObjectWithTag("Boss").GetComponent<All_Enemies_Health>();          
            ran = true;
        }

        if (show == false && destroyerHp != null && destroyerHp.enabled == true)
        {
            bar.enabled = true;
            show = true;
        }
      

        if (show == true)
        {
            bar.fillAmount = destroyerHp.enemyHp / 1500f;
        }
        
    }

    void Reference()
    {

    }
}
