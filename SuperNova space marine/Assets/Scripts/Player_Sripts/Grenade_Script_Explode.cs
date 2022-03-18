using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using EZCameraShake;

public class Grenade_Script_Explode : MonoBehaviour
{
    float timer = 1; //grenade explodes after 1 second;
    float countdown;
    bool exploded;
    public float radius;
    public int grenadeDamage;
    public GameObject explosionParticle;
    public string[] tags;

    void Start()
    {

        countdown = timer;
    }
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !exploded)
        {
            
            Explode();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !exploded)
        {

            Explode();
        }
    }

    void Explode()
    {
        CameraShaker.Instance.ShakeOnce(5, 6, 0.1f, 0.4f); //shakes the camera
        SFX_Manager.instance.Play("Grenade_Explode");
        GameObject effect = Instantiate(explosionParticle, transform.position, transform.rotation); // instantiates an explosion animation   
        effect.transform.localScale = new Vector3(radius, radius,0);
        Destroy(effect, 0.26f); //destroys the animation after 0.26 seconds

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius); //makes an array of all the 2d colliders in the overlapcircle

        //CHANGE THE CODE TO THE FOLLOWING COMMENTED SECTION TO ALLOW MORE THAN JUST
        //THE ENEMIES TO TAKE DAMAGE
        
            foreach (Collider2D obj in colliders)
            {
            for (int i = 0; i < tags.Length; i++)
            {
                if (obj.CompareTag(tags[i]))
                {
                    //we deal damage
                    obj.GetComponent<EnemyStats>().TakeDamage(grenadeDamage);
                    exploded = true;
                }
            }
            }
        Destroy(gameObject); //destroys the grenade itself
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}
}
