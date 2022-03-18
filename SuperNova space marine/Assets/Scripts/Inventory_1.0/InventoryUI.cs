using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour
{
    #region Variables
    Inventory_1 inventory;
    private Transform itemsParent;
    public InventorySlot[] slots;
    private GameObject inventoryUI;
    private Canvas InventoryUICanvas;
    Scene loadedScene;
    private bool invVisible = true;//at the start, by default, the inventory is onn. We'll set this to false when we hide the inventory.
    public static InventoryUI instance;

    public bool open = false;
    #endregion
    //Functions


    void Awake()
    {
        if (instance != null && instance != this)//already exists an instance of InventoryUI
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
        inventoryUI = GameObject.FindGameObjectWithTag("Inventory_UI");
        itemsParent = inventoryUI.transform.Find("Items Parent");
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        GameObject temp = GameObject.Find("InventoryUI Canvas");
        InventoryUICanvas = temp.GetComponent<Canvas>();
        DontDestroyOnLoad(InventoryUICanvas.gameObject);
    }
    void Start()
    {
        inventory = Inventory_1.instance; //cache of the Inventory_1 instance
        inventory.onItemChangedCallBack += UpdateUI; //adding UpdateUI to the delegate
        ToggleInventory();
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory();
        }
    }

    #region Scene Change Detection
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//called when a scene is changed.
    {
        loadedScene = SceneManager.GetActiveScene();
        if (loadedScene.name != "Main_Menu" && loadedScene.name != "Intro_Movie")//include scenes where the player should not be able to open their inventory
        {
            if (!inventoryUI.activeInHierarchy)
            {
                inventoryUI.SetActive(true);
            }
            if (invVisible == true)
            {
                ToggleInventory();
            }
        }
    }
    #endregion

    public void ToggleInventory()
    {
        //bring up the inventory UI or close it
        if (invVisible == true)
        {
            for (int i = 0; i < inventoryUI.transform.childCount; i++)
            {
                inventoryUI.transform.GetChild(i).gameObject.SetActive(false);//turns off all inventoryUI children
            }
            itemsParent.gameObject.SetActive(true);//re-enabling itemsParent to see the hotbar
            for (int i = 0; i < itemsParent.childCount; i++)
            {
                itemsParent.GetChild(i).gameObject.SetActive(false);//disabling all itemsParent children
            }
            itemsParent.Find("HotbarWeaponSlotHolder").gameObject.SetActive(true);//re-enabling the hotbar.
            itemsParent.Find("HotbarAbilitySlotHolder").gameObject.SetActive(true);
            invVisible = false;
            open = false;
        }
        else
        {
            for (int i = 0; i < inventoryUI.transform.childCount; i++)
            {
                inventoryUI.transform.GetChild(i).gameObject.SetActive(true);//turns onn all inventoryUI children
            }
            for (int i = 0; i < itemsParent.childCount; i++)
            {
                itemsParent.GetChild(i).gameObject.SetActive(false);//disabling all itemsParent children
            }
            itemsParent.Find("HotbarWeaponSlotHolder").gameObject.SetActive(true);//re-enabling the hotbar.
            itemsParent.Find("HotbarAbilitySlotHolder").gameObject.SetActive(true);
            itemsParent.GetChild(0).gameObject.SetActive(true);//enabling the first slots holder in the hierarchy (probably the default slots holder)
            invVisible = true;
            open = true;
        }
    }
    
    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++) //loops through all the slots
        {
            if (i < inventory.items.Length) //doesn't allow i to go above the max number of slots
            {                              
                if (inventory.items[i] != null)
                {
                    slots[i].AddItem(inventory.items[i]); //adds item of index i to slot of index i
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }
    }
}
