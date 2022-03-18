using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler 
{
    //Variables
    public Item item;
    public Image icon;
    //public Button removeButton;
    public SlotType slotType;
    public string weaponSlotID;//Numbered from 1 to 9 for weapon slots
    public KeyCode slotKeyBind; //Key bind to equip weapon, use ability etc.
    public bool activatedByKeyBind;

    //Functions

    public void AddItem(Item newItem) //visual representation of adding an item
    {                                 //to an inventory
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        //removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        
    }
    public void OnRemoveButton()
    {
        Inventory_1.instance.RemoveItem(item);
    }

    public void UseItem() //Executed when clicked on an item in inventory
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void OnDrop(PointerEventData eventData)//is called when item is dropped on top of slot.
    {
        if (eventData.pointerDrag != null)
        {
            InventorySlot draggedSlot = eventData.pointerDrag.GetComponentInParent<InventorySlot>();
            if (draggedSlot.item != null)
            {
                if (this.slotType == draggedSlot.slotType || this.slotType == SlotType.hotbarWeapon && draggedSlot.slotType == SlotType.inventoryWeapon || this.slotType == SlotType.inventoryWeapon && draggedSlot.slotType == SlotType.hotbarWeapon)
                {
                    if (item == null)//destination item
                    {
                        int destinationSlotIndex = System.Array.IndexOf(InventoryUI.instance.slots, this);//grabs index of destination SLOT
                        Inventory_1.instance.MoveItemAtIndex(draggedSlot.item, destinationSlotIndex);
                        AddItem(draggedSlot.item);//add item to destination slot
                        draggedSlot.ClearSlot();
                        Debug.Log(destinationSlotIndex);
                    }
                    else
                    {
                        int destinationIndex = System.Array.IndexOf(Inventory_1.instance.items, item);//grabs index of destination
                        Inventory_1.instance.MoveItemAtIndex(draggedSlot.item, destinationIndex);
                        Item bufferItem = item;
                        AddItem(draggedSlot.item);//add item to destination slot
                        draggedSlot.AddItem(bufferItem);
                        Debug.Log(destinationIndex);
                    }
                }
            }
        }
    }

   
}

public enum SlotType {Default, Head, Chest, Legs, Consumable, Ability, inventoryWeapon, hotbarWeapon}
