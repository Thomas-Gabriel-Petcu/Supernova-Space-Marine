using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]

public class Interactable : MonoBehaviour
{
    //variables

    public float radius;
    bool hasInteracted = false;
    CircleCollider2D col;


    //functions
    void Awake()
    {
        col = gameObject.GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = radius;
    }

    void Update()
    {
        if (hasInteracted == true)
        {
            hasInteracted = false;
            Interact();
        }
    }

    public virtual void Interact()
    {
        hasInteracted = false;
        //Debug.Log("Interacted");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            hasInteracted = true;
            //no reason for checking for exit and setting hasInteracted to false as the item
            //would be picked up each time
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
