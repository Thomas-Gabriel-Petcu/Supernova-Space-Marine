using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer_Projectile : MonoBehaviour
{
    public float speed;
    public Transform player;
    public Vector3 target;
    public Player_Health playerHealth;
    public float enemyDamage;
    public float lifeTime;

    public GameObject audioObject;

    public GameObject effect;
    public float radius;
    
    Vector3 direction;
    float angle;

    void Awake()
    {

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>(); //grabs reference to player's health script
        player = GameObject.FindGameObjectWithTag("Player").transform; //grabs reference to player's transform

    }

    void Start()
    {
        enemyDamage = Random.Range(10, 20);
        direction = player.position - transform.position; //calculates direction to player
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //calculates angle to player
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //rotates the projectile to face the player
        Invoke("Explode", lifeTime);
        
    }

   
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);       
    }



    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            Explode();

        }

    }

    void Explode()
    {
        
        GameObject audioHolder = Instantiate(audioObject, transform.position, Quaternion.identity);
        Destroy(audioHolder, 1f);

        GameObject effectHolder = Instantiate(effect, transform.position, transform.rotation);
        effect.transform.localScale = new Vector3(radius, radius, 0);
        Destroy(effectHolder, 1f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach ( Collider2D otherObject in colliders)
        {
            Rigidbody2D rb = otherObject.GetComponent<Rigidbody2D>();
            if (otherObject.CompareTag("Player"))
            {
                otherObject.GetComponent<Player_Health>().health -= enemyDamage;

            }
        }

        DestroyProj();
    }

    void DestroyProj()
    {
        transform.position = GameObject.FindGameObjectWithTag("Trash_Dump").transform.position;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
