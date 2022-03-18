using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectile : MonoBehaviour {
    public float projectileDamage;
    public float speed;
    public float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    
}
