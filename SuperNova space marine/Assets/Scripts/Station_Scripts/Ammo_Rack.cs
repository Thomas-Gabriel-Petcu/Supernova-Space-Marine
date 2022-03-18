using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Rack : MonoBehaviour
{
    public RuntimeAnimatorController controller;
    public Sprite weaponRack2;
    public SpriteRenderer sRend;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                other.GetComponent<Animator>().runtimeAnimatorController = controller;
                sRend.sprite = weaponRack2;
            }
        }
    }
}
