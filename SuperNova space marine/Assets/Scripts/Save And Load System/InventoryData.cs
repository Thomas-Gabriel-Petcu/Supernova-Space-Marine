using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public bool[] items;
    public int[] IDs;
  public InventoryData (Inventory_1 inventory)
    {
        items = new bool[inventory.slotCount];//initializing items array
        IDs = new int[inventory.slotCount];
        for (int i = 0; i < items.Length; i++)
        {
            if (inventory.items[i] != null)
            {
                items[i] = true;//allows us to know what indexes of the inventory items array were occupied by items.
                IDs[i] = inventory.items[i].ID;
            }
        }
    }
}
