using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer_Laser_Proj : MonoBehaviour
{
    public float speed;
    float enemyDamage;
    public float lifetime;

    Transform player;
    float angle;
    Vector3 direction;
    //Add GameObject variable to hold impact effect

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //grabs reference to player's transform
        Invoke("DestroyProject", lifetime);
        direction = player.position - transform.position; //calculates direction to player
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //calculates angle to player
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //rotates the projectile to face the player
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyDamage = Random.Range(1,5);
            other.GetComponent<Player_Health>().health -= enemyDamage;
            //Instantiate impact effect
            DestroyProject();
        }
    }

    void DestroyProject()
    {

        transform.position = GameObject.FindGameObjectWithTag("Trash_Dump").transform.position;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
