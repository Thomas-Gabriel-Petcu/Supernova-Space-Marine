using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt_Shooting : MonoBehaviour
{
    public PolygonCollider2D col;
    public EnemyStats enemyStats;
    public Grunt_Movement movement;
    public GameObject gruntProj;
    public float timeBtwShots;
    public float startTimeBtwShots;
    public Transform firePos;
    void Awake()
    {
        enabled = false; //disables the script upon start
    }
    void OnBecameVisible()
    {
        enabled = true; //enables the script when the object is visible to the player
    }
    void Update()
    {
        if (timeBtwShots <= 0 && enemyStats.currentHealth > 0)
        {
            SFX_Manager.instance.Play("Lasership_Fire");

            Instantiate(gruntProj, firePos.position, Quaternion.identity); //instantiates a projectile
            timeBtwShots = startTimeBtwShots; //resets time between shots counter           
        }else
        {        
            timeBtwShots -= Time.deltaTime; //decreases the counter over time after firing
        }
        if (enemyStats.currentHealth <=0)
        {
            movement.enabled = false;
            col.enabled = false; //why does the shooting script handle dying?
        }
    } 
}
