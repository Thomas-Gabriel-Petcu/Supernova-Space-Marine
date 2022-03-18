using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Flash : MonoBehaviour
{
    public Material matWhite;
    public Material matDefault;
    SpriteRenderer rend;
    private EnemyStats enemyStats;
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        rend = GetComponent<SpriteRenderer>();
        matDefault = rend.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStats.isHit == true)
        {
            rend.material = matWhite;
            Invoke("Reset", 0.1f);
            enemyStats.isHit = false;
        }
    }

    void Reset()
    {
        rend.material = matDefault;
    }
}
