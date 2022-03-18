using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer_Shooting : MonoBehaviour
{
    public float tBtwShots;
    public float sTBtwShots;
    public GameObject destroyerProj;
    public Transform firePos1;
    public Transform firePos2;

    public GameObject audioObject;
    public GameObject fireEffect;



    void Start()
    {
        enabled = false; //disables the shooting of the destroyer at the start of the game
    }
    
    void OnBecameVisible()
    {
        enabled = true; //enables the destroyer's shooting when it becomes visible to the player
        
    }
   
    void Update()
    {
        StartCoroutine(Shooting());

            tBtwShots -= Time.deltaTime;
       
    }


    IEnumerator Shooting()
    {
        
        yield return new WaitForSeconds(tBtwShots);
        

        if (tBtwShots <= 0)
        {
            tBtwShots = sTBtwShots;
            Instantiate(audioObject, transform.position, Quaternion.identity);   
            Instantiate(destroyerProj, firePos1.position, Quaternion.identity);
            GameObject effectHolder = Instantiate(fireEffect, firePos1.position, transform.rotation);
            Destroy(effectHolder, 0.26f);

            yield return new WaitForSeconds(1f);

            Instantiate(audioObject, transform.position, Quaternion.identity);
            Instantiate(destroyerProj, firePos2.position, Quaternion.identity);
             effectHolder = Instantiate(fireEffect, firePos2.position, transform.rotation);
            Destroy(effectHolder, 0.26f);
        }
       
        
        
    }
}
