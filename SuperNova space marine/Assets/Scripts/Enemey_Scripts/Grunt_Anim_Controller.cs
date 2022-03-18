using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt_Anim_Controller : MonoBehaviour
{

   public Animator anim;
   public Animator engineAnim;
   public Animator fireAnim;
   public Grunt_Shooting mGrunt_Shooting;
    public EnemyStats enemyStats;

    void Start()
    {
        mGrunt_Shooting = gameObject.GetComponentInParent<Grunt_Shooting>();
    }
    void Update()
    {
        if (mGrunt_Shooting.timeBtwShots <= 0 && enemyStats.currentHealth > 0)
        {
            fireAnim.SetBool("isFiring", true);
        }
        else
        {
            fireAnim.SetBool("isFiring", false);
        }

        if (enemyStats.currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
            engineAnim.SetBool("stopEngine", true);
           
           
        }
    }
}
