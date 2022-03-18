using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool defaultItem = false;
    public bool isStackable = false;
    public int maxStack;
    public ItemType itemType;
    public float x, y, z, distance; //coordinates to offset equipable item's position
    public int ID = 1;

    public virtual void Use()
    {
        //use item
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory_1.instance.RemoveItem(this);
    }

}

public enum ItemType { Default, Head, Chest, Legs, Consumable, Ability, Weapon}
