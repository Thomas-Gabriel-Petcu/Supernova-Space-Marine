using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio : MonoBehaviour
{
  
    public Player_Movement pm;   
    public Rigidbody2D rb; 
    public AudioSource audioSrc;

    void Update()
    {
        
        if (pm.isGrounded && pm.moving && !audioSrc.isPlaying)
        {
            //we're grounded and moving - play footsteps
            audioSrc.Play();
            
        }

    }
}
