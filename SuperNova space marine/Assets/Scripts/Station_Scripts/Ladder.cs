using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    float climbSpeed = 2f;


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
               
                sr.sortingLayerName = "MIDGROUND";           
                GetComponent<Player_Movement>().enabled = false;
                rb.gravityScale = 0;
                transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
                rb.velocity = Vector2.up * climbSpeed;
                          
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {

                GetComponent<Player_Movement>().enabled = false;                         
                rb.gravityScale = 0;
                transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
                rb.velocity = -Vector2.up * climbSpeed;
                
            }
           
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //GetComponent<Player_Movement>().enabled = true;
        //rb.gravityScale = 1;

    }
}
