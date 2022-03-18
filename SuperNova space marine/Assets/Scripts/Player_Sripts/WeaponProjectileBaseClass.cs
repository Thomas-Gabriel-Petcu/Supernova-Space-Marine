using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WeaponProjectileBaseClass : MonoBehaviour
{
    //variables
    [Header("Basics")]
    public float lifeSpan;
    public float speed;
    public int damage;
    [Header("Type")]
    public bool canPassThroughTargets;
    public bool canTrack;
    public bool isExplosive;
    public bool hitOverTime;
    [Header("Special properties")]
    public float rotationSpeed;
    public float explosionRadius;
    float angle;
    bool hasExploded = false;
    Vector3 direction;
    [Header("References")]
    public List<Collider2D> colliders;
    public Rigidbody2D rb;
    Transform target;
    [SerializeField] private LayerMask bitMask;
    private bool cr_running = false;


    //functions
    private void Start()
    {
        colliders = new List<Collider2D>();
    }

    void Update()
    {
        if (canTrack)
        {
            Track();
        }
    }
    void FixedUpdate()
    {
        rb.velocity = transform.right * speed * Time.deltaTime;
    }

    void Track()
    {
        direction = target.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
    }

    void Explode()
    {
        if (hasExploded == false)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                if ((bitMask.value & 1 << colliders[i].gameObject.layer) != 0)
                {
                    colliders[i].GetComponent<EnemyStats>().TakeDamage(damage);
                }
            }
            //play an explosion animation
            //play an explosion sound
            hasExploded = true;
            Disable();
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (isExplosive == false)
        {
            if ((bitMask.value & 1 << obj.gameObject.layer) != 0)
            {
                if (canPassThroughTargets)
                {
                    colliders.Add(obj);
                    if (cr_running == false)
                    {
                        cr_running = true;
                        StartCoroutine("DamageOverTime");
                        Debug.Log("started coroutine");
                    }
                }
                else
                {
                    obj.GetComponent<EnemyStats>().TakeDamage(damage);
                    Disable();
                }
            }
            //I probably want to add some sort of impact animation as well!
        }
        else
        {
            if ((bitMask.value & 1 << obj.gameObject.layer) != 0)
            {
                Explode();
            }
        }
    }

    void OnEnable()
    {
        hasExploded = false;
        if (isExplosive == false)
        {
            Invoke("Disable", lifeSpan);
        }
        else
        {
            Invoke("Explode", lifeSpan);
        }
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    IEnumerator DamageOverTime()
    {
        while (cr_running == true)
        {
            if (colliders.Count < 1)
            {
                Debug.Log("stopped coroutine");
                cr_running = false;
                StopCoroutine("DamageOverTime");
            }
            for (int i = 0; i < colliders.Count; i++)
            {
                if (colliders[i] != null)
                {
                    Debug.Log($"dealt damage to {colliders[i]}");
                    colliders[i].GetComponent<EnemyStats>().TakeDamage(damage);
                }
                else
                {
                    colliders.RemoveAt(i);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
