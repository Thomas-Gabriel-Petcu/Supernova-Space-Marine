using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class All_Enemies_Health : MonoBehaviour
{
    public int rand;
    public GameObject[] loot;
    public float enemyHp;
    bool alive = true;
    public bool isHit = false;

    public float magnitude;
    public float roughness;
    public float fadeIn;
    public float fadeOut;

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
        if (enemyHp <= 0 && alive == true)
        {
            DeathLootDrop();
            alive = !alive;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player_Proj"))
        {
            isHit = true;
            enemyHp -= other.GetComponent<Player_Projectile>().projectileDamage;
            Destroy(other.gameObject);
            if (enemyHp <= 0)
            {
                DeathLootDrop();
                CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeIn, fadeOut);
            }
        }

    }

    void DeathLootDrop()
    {
        rand = Random.Range(0, loot.Length);
        Instantiate(loot[rand], transform.position, transform.rotation);
        Invoke("Delayer", 0.7f);
        
    }

    void Delayer()
    {       
        Destroy(gameObject);
    }

}
