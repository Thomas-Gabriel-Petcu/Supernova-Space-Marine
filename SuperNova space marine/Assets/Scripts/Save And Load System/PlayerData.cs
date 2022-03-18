using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
   

    public float[] position;
    public int weaponID;
    public float currentPlayerHealth;
    //public float maxPlayerHealth; //I don't think I need to save the maxPlayerHealth because it is determined by modifiers of the Stats anyway
    //so if I save the Equiped items (If they have health modifiers) the maxhealth will be retained properly

    public PlayerData(PlayerManager playerManager)
    {
        position = new float[3];

        position[0] = playerManager.transform.position.x;
        position[1] = playerManager.transform.position.y;
        position[2] = playerManager.transform.position.z;

        if (playerManager.transform.GetChild(0).transform.childCount == 2)//there are 2 objects on the player arm, one of which is guaranteed to be a weapon
        {
            weaponID = playerManager.transform.GetChild(0).GetChild(1).GetComponent<FireArmBaseClass>().ID;//grabs ID of the weapon
        }
        currentPlayerHealth = playerManager.playerStats.currentHealth;
    }
}
