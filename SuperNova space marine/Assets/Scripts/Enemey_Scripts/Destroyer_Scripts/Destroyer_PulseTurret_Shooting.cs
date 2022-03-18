using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer_PulseTurret_Shooting : MonoBehaviour
{
    //----------- Handles time between shots
    public float tBtwShots;
    public float sTBtwShots;
    //----------

    //----------- holds objects and fire position
    public GameObject audioObject;
    public GameObject projectile;
    public Transform firePos1;
    //----------

    //------- fire rate proportional to health stuff
    All_Enemies_Health parentHealth;
    float value;
    float value2;
    //----------

    SpriteRenderer rend;
    float timer = 12f;

    void Start()
    {
        rend = GetComponentInParent<SpriteRenderer>();
        value = sTBtwShots;
        value2 = value;
        parentHealth = GetComponentInParent<All_Enemies_Health>();
        enabled = false; //disables the shooting of the destroyer at the start of the game
    }

    void OnBecameVisible()
    {
        enabled = true; //enables the destroyer's shooting when it becomes visible to the player

    }

   
    void Update()
    {
        timer -= Time.deltaTime;
        StartCoroutine(Shooting());

        if (timer <= 3)
        {
            rend.color = Color.red;
            value = 0.4f;
            if (timer <= 0)
            {
                timer = 10;
                value = value2;
            }
            
        }

        tBtwShots -= Time.deltaTime;
        sTBtwShots = value -(1500 - parentHealth.enemyHp) / 1000;
        if (sTBtwShots < 0.6f)
        {
            sTBtwShots = 0.6f;
        }
    }

    IEnumerator Shooting()
    {

        yield return new WaitForSeconds(tBtwShots);


        if (tBtwShots <= 0)
        {
            tBtwShots = sTBtwShots;
            Instantiate(audioObject, transform.position, Quaternion.identity);
            Instantiate(projectile, firePos1.position, transform.rotation * Quaternion.Euler(0f,0f,90f));

            yield return new WaitForSeconds(0.1f);
  
            Instantiate(projectile, firePos1.position, transform.rotation * Quaternion.Euler(0f, 0f, 90f));

            yield return new WaitForSeconds(0.1f);

            Instantiate(projectile, firePos1.position, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
        }



    }
}
