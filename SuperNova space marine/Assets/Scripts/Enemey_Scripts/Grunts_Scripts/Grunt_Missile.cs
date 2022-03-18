using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Grunt_Missile : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;
    public int missileDamage;
    public int radius;
    public bool hasExploded;
    public float lifeTime;
    public GameObject effect;
    void Start()
    {
        Invoke("Explode", lifeTime);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //grabs a reference to the player's transform - active seeking
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime); // moves towards the player
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasExploded || other.CompareTag("Player_Proj") && !hasExploded) //checks if missile collided with player and hasn't exploded
        {

            Explode(); //calls Explode() function
        }
    }

    void Explode()
    {
        CameraShaker.Instance.ShakeOnce(4,5,0.1f,0.3f);
        SFX_Manager.instance.Play("Grunt_Missile");
        GameObject effectHolder = Instantiate(effect,transform.position, transform.rotation); // instantiates an explosion effect
        effectHolder.transform.localScale = new Vector3(radius, radius, 0);
        Destroy(effectHolder, 0.26f); //destroys explosion effect after 0.26 seconds

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius); //creates an overlap circle and all colliders inside it are placed inside a Colliders2D array
        foreach (Collider2D nearbyObject in colliders)        
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>(); //grabs reference to Rigidbody2D component in each object
            if (nearbyObject.CompareTag("Player") && rb != null ) //checks if object in array is the player and if it has a Rigidbody2D
            {
                nearbyObject.GetComponent<PlayerStats>().TakeDamage(missileDamage); //deals damage to the player
                hasExploded = true; //boolean indicating that the missile has exploded - prevents multiple explosions in Update()
                
            }
        }
        DestroyObject(); //calls destroy function
    }
    void DestroyObject()
    {
        //transform.position = GameObject.FindGameObjectWithTag("Trash_Dump").transform.position;
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
