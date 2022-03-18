using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void PickUp()
    {
        
        bool wasPickedUp = Inventory_1.instance.AddItem(item);
        if (wasPickedUp)
        {
            Debug.Log("Picking up " + item.name);
            Destroy(gameObject);
        }

    }
}
