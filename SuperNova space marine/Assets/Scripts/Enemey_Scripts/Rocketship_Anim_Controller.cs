using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocketship_Anim_Controller : MonoBehaviour
{
    public Animator animCtrl;
    public Animator fireAnimCtrl;
    public Animator engineAnimCtrl;
    public Animator engineAnimCtrl2;
    public EnemyStats enemyStats;
    public Grunt_Missile_Launch fireCS;


    void Update()
    {
        if (fireCS.timeBtwShots <= 0 && enemyStats.currentHealth >0)
        {
            fireAnimCtrl.SetBool("isFiring", true);
        }
        else
        {
            fireAnimCtrl.SetBool("isFiring", false);
        }
        if (enemyStats.currentHealth <= 0)
        {
            animCtrl.SetBool("isDead", true);
            engineAnimCtrl.SetBool("stopEngine", true);
            engineAnimCtrl2.SetBool("stopEngine", true);
        }
    }
}
