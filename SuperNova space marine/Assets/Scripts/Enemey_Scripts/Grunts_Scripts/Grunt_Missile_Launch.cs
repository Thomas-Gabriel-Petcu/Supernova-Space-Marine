using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt_Missile_Launch : MonoBehaviour
{
    public Grunt_Movement movement;
    public PolygonCollider2D col;
    public EnemyStats enemyStats;
    public GameObject missile;
    public float timeBtwShots;
    public float startTimeBtwShots;
    public Transform firePos;

    void Start()
    {
        enabled = false;
    }
    void OnBecameVisible()
    {
        enabled = true;
    }
    void Update()
    {
        if (timeBtwShots <= 0 && enemyStats.currentHealth > 0)
        {
            SFX_Manager.instance.Play("Grunt_Missile_Launch");
            Instantiate(missile, firePos.position, transform.rotation);
            timeBtwShots = startTimeBtwShots;
        }else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (enemyStats.currentHealth <=0)
        {
            movement.enabled = false;
            col.enabled = false;
        }
    }
}
