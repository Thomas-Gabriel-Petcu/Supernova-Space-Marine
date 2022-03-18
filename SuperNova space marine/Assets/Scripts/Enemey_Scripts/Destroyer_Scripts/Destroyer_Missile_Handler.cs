using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer_Missile_Handler : MonoBehaviour
{
    public List<GameObject> missiles = new List<GameObject>();
    int counter = 4;
    public GameObject n;
    public GameObject firePos;
    public GameObject initialPos;
    public Vector2 newPos;
    public bool isReplenished = false;

    All_Enemies_Health health;
    void Start()
    {
        health = GetComponentInParent<All_Enemies_Health>();
        newPos = new Vector2(transform.position.x + 1f, transform.position.y);
        initialPos.transform.position = firePos.transform.position;
        
    }

    
    void Update()
    {
        if (missiles.Count >3)
        {
            isReplenished = true;
        }
        else if (missiles.Count <1)
        {
            isReplenished = false;
            
        }
        if (health.enemyHp <= 100)
        {
            foreach (GameObject missile in GameObject.FindGameObjectsWithTag("BossMissileLeft"))
            {
                Boss_Missle_Explode explodeCS = missile.GetComponent<Boss_Missle_Explode>();
                explodeCS.lifeTime = 0;
            }
            foreach (GameObject missile in GameObject.FindGameObjectsWithTag("BossMissileRight"))
            {
                Boss_Missle_Explode explodeCS = missile.GetComponent<Boss_Missle_Explode>();
                explodeCS.lifeTime = 0; 
            }
        }
    }

    public void FireMissiles()
    {
        StartCoroutine("StaggerFire");

    }

   public void Replenish()
    {
        firePos.transform.position = initialPos.transform.position;
        for (int i = 0; i < counter; i++)
        {
            Instantiate(n, firePos.transform.position, n.transform.rotation = Quaternion.Euler(0,0,-90f));
            firePos.transform.position = new Vector3(firePos.transform.position.x + 0.35f,firePos.transform.position.y,firePos.transform.position.z);         
        }
        if (gameObject.CompareTag("RightLauncher"))
        {
            RightSearch();
        }
        if (gameObject.CompareTag("LeftLauncher"))
        {
            LeftSearch();
        }
       
        
    }

    IEnumerator StaggerFire()
    {
        foreach (GameObject missile in missiles)
        {
            
           yield return new WaitForSeconds(0.4f);
           Boss_Missile_Homing homingCS = missile.GetComponent<Boss_Missile_Homing>();
           Boss_Missle_Explode explodeCS = missile.GetComponent<Boss_Missle_Explode>();
           homingCS.enabled = true;
           explodeCS.enabled = true;
           
        }
        missiles.Clear();
        StopCoroutine("StaggerFire");
    }

    void LeftSearch()
    {
        foreach (GameObject missile in GameObject.FindGameObjectsWithTag("BossMissileLeft"))
        {
            missiles.Add(missile);
        }
    }

    void RightSearch()
    {
        foreach (GameObject missile in GameObject.FindGameObjectsWithTag("BossMissileRight"))
        {
            missiles.Add(missile);
        }
    }
}
