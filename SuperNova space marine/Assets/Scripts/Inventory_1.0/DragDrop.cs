using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour,IPointerDownHandler,IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private InventoryUI invUI;
    private Inventory_1 inv;
    private InventorySlot slot;
    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvas = GameObject.Find("InventoryUI Canvas").GetComponent<Canvas>();//Inefficient memory wise but it's the main menu so it doesn't matter.
    }

    void Start()
    {
        invUI = InventoryUI.instance;
        inv = Inventory_1.instance;
        slot = GetComponentInParent<InventorySlot>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (invUI.open == true)
        {
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (invUI.open == true)
        {
            //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor / InventoryUI.instance.gameObject.transform.localScale; //  / InventoryUI.instance.gameObject.transform.localScale *1.04f
            gameObject.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts =true;
        rectTransform.anchoredPosition = new Vector2(0, 0);//move the item button back in original position
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            OnShiftClickOnSlot();
        }
    }
    public void OnShiftClickOnSlot()
    {
        int originalIndex = System.Array.IndexOf(invUI.slots, slot);
        Debug.Log(originalIndex);
        Debug.Log(invUI.slots.Length / 2);
        if (originalIndex < invUI.slots.Length - 9)
        {
            for (int i = invUI.slots.Length - 1; i >= 0; i--)//reverse for loop
            {
                if (invUI.slots[i].slotType == slot.slotType && invUI.slots[i].item == null || invUI.slots[i].item == null && invUI.slots[i].slotType == SlotType.hotbarWeapon && slot.slotType == SlotType.inventoryWeapon || invUI.slots[i].item == null && invUI.slots[i].slotType == SlotType.inventoryWeapon && slot.slotType == SlotType.hotbarWeapon)
                {
                    inv.MoveItemAtIndex(slot.item, i);
                    invUI.slots[i].AddItem(slot.item);
                    invUI.slots[originalIndex].ClearSlot();
                    break;
                }
            }
        }
        else if(originalIndex >= invUI.slots.Length - 9)
        {
            for (int i = 0; i < invUI.slots.Length; i++)
            {
                if (invUI.slots[i].slotType == slot.slotType && invUI.slots[i].item == null || invUI.slots[i].item == null && invUI.slots[i].slotType == SlotType.hotbarWeapon && slot.slotType == SlotType.inventoryWeapon || invUI.slots[i].item == null && invUI.slots[i].slotType == SlotType.inventoryWeapon && slot.slotType == SlotType.hotbarWeapon)
                {
                    inv.MoveItemAtIndex(slot.item, i);
                    invUI.slots[i].AddItem(slot.item);
                    invUI.slots[originalIndex].ClearSlot();
                    break;
                }
            }
        }
    }
}
