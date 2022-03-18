using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Universal_Enemy_Health : MonoBehaviour
{
    public float destroyerHp;
    private Image bossBar;
    void Awake()
    {
        bossBar = GameObject.FindGameObjectWithTag("BossHp").GetComponent<Image>();
       
    }
    void Start()
    {
        
        destroyerHp = 1000f;
       
    }

    void OnBecameVisible()
    {
        bossBar.GetComponent<Image>().enabled = true;
    }
    void Update()
    {
        bossBar.fillAmount = destroyerHp / 1000f;


        if (destroyerHp <= 0)
        {
            Destroy(gameObject); //add death animation in the future
        }
      
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player_Proj"))
        {
            destroyerHp -= other.GetComponent<Player_Projectile>().projectileDamage;
        }

    }

    }
