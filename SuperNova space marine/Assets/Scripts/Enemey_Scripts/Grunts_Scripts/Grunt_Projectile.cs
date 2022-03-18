using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt_Projectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    public Vector3 target;
    public int enemyDamage;
    public float lifeTime;

    Vector3 direction;
    float angle;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        enemyDamage = Random.Range(2, 6);
        direction = player.position - transform.position; //calculates direction to player
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //calculates angle to player
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //rotates the projectile to face the player
        Invoke("DestroyProj", lifeTime);

    }


    void Update()//this should probably be fixed update?
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerStats>().TakeDamage(enemyDamage);
            DestroyProj();//Change to object pooling in the future
        }
    }

    void DestroyProj()
    {
        transform.position = GameObject.FindGameObjectWithTag("Trash_Dump").transform.position;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}




