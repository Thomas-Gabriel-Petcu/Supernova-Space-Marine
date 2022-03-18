using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
        //play some sort of death animation like an explosion or something
    }

}
