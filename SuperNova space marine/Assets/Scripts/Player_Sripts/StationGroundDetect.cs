using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationGroundDetect : MonoBehaviour
{
    public Player_Movement movement;
    public SpriteRenderer sr;
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.collider.CompareTag("LadderBottomCheck") && sr.sortingLayerName == "MIDGROUND")
        {

            sr.sortingLayerName = "Player";
            movement.enabled = true;
        }


    }

    void OnCollisionStay2D(Collision2D other)
    {

        if (other.collider.CompareTag("LadderBottomCheck") && !movement.enabled && sr.sortingLayerName != "MIDGROUND")
        {
            movement.enabled = true;
        }
        if (other.collider.CompareTag("LadderBottomCheck1"))
        {
            movement.enabled = true;
        }

    }
    


}
