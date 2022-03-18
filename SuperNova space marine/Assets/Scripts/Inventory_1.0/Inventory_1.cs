using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_1 : MonoBehaviour //this script must be on an inventory manager
{
    #region Singleton And References
    public static Inventory_1 instance;
    InventoryUI inventoryUIinstance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("More than 1 instance of inventory");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        inventoryUIinstance = InventoryUI.instance;//cache of InventoryUI instance
        items = new Item[slotCount];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = null;
        }
    }
    #endregion
    #region Variables
    public int slotCount;
    public Item[] items;
    public Item[] allItems;
    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallBack;
    #endregion
    //Functions

    private void Start()
    {
        for (int i = 0; i < allItems.Length; i++)
        {
            allItems[i].ID = i + 1;//must not start at 0
        }
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventoryUIinstance.slots.Length; i++)//loops through all UI slots
        {
            if ((int)item.itemType == (int)inventoryUIinstance.slots[i].slotType && inventoryUIinstance.slots[i].item == null)//checks if the item and slot are of the same type
            {
                if (!item.defaultItem)//checks if item is not a default item. The fuck does that even mean?
                {
                    int nullcounter = 0;//initializing variable for counting how many nulls are in the inventory
                    for (int j = 0; j < items.Length; j++)
                    {
                        if (items[j] == null) //check if items in the inventory are null
                        {
                            nullcounter += 1; //if they are then the slot is empty and null counter increases
                        }
                    }
                    if (nullcounter == 0) //if there are no null slots then the inventory is full.
                    {
                        Debug.Log("Not enough room");
                        return false;
                    }
                    else
                    {
                        items[i] = item;
                        Debug.Log("Added " + item + "to slot index number " + i);
                    }
                    if (onItemChangedCallBack != null)
                    {
                        onItemChangedCallBack();
                    }
                    break;
                }
            }
        }
        return true;
    }
    
    public void RemoveItem(Item item)
    {
        var temp = System.Array.IndexOf(items,item);//grabs index of item
        items[temp] = null;//sets item to null
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack();
        }
    }

    public void MoveItemAtIndex(Item item ,int index)//swaps items if there's an item at destination
    {
        int originalIndex = System.Array.IndexOf(items, item);
        if (item != null)
        {
            if (items[index] == null)//checks if item at destination is null
            {
                items[index] = item;//move the item at specified index
                items[originalIndex] = null;
                Debug.Log("The destination item was null and we moved the target item");
            }
            else if (items[index] != null)//checks if item at destination is not null
            {
                Item bufferItem = items[index];//buffer item to perform the swap
                items[index] = item;//move the item at specified index
                items[originalIndex] = bufferItem;
                Debug.Log("Destination item was not null so it got swaped with the target item");
            }
        }
    }
}
