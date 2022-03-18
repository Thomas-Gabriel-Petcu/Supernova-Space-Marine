using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Box_Script : MonoBehaviour
{
    public Transform followTarget;
    public SpriteRenderer sRend;
    public Sprite[] sprites;
    int rand;
    
    void Update()
    {
        transform.position = followTarget.position + new Vector3(0,1,0);
        rand = Random.Range(0, sprites.Length);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sRend.sprite = sprites[rand];
            sRend.enabled = true; //enables the sprite renderer of the dialogue box when in contact with the player
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            
            sRend.enabled = false;//disables the sprite renderer of the dialogue box when the player leaves
        }
    }
}
