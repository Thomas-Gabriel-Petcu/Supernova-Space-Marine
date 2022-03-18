using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAll : MonoBehaviour
{

    private void Start()
    {
        Debug.LogError("Developer tool is active. Press K to kill all enemies");
        Debug.LogError("Developer tool is active. Press E heal the player for 50 health");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponentInParent<Player_Health>().health += 50;
        }
    }
}
