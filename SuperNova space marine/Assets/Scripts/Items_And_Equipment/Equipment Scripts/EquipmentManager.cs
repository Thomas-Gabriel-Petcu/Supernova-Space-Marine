using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this);
        }
    }
    #endregion

    #region Scene Changed Detection

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Main_Menu")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerArm = player.transform.GetChild(0);
            playerArmCS = playerArm.GetComponent<PlayerArm>();
            playerStats = player.GetComponent<PlayerStats>();
        }
        //Debug.Log("We found a reference for: " + player);
        //Why the fuck did I do this part with the scene change detection??
        //I think I made this to detect the inventoryUI when the scene changed because we cannot use dontdestroyonload for canvas
        //You can use dontdestroyonload for canvas, dumbass.
    }

    #endregion
    //Variables

    public List<GameObject> weaponObjects;
    public GameObject[] abilityObjects;
    Equipment mWeapon;
    Equipment[] currentEquipment;
    Inventory_1 inventory;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChangedCallBack;
    GameObject player;
    public Transform playerArm;
    public PlayerArm playerArmCS;
    private InventoryUI inventoryUIinstance;
    public List<InventorySlot> keyBindActivatedSlots;
    PlayerStats playerStats;
    [HideInInspector]
    public float distance;

    public KeyCode[] keyCodes;

    private float timer = 0;
    private GameObject weaponPool;
    //Functions

    void Start()
    {
        weaponPool = gameObject.transform.Find("WeaponPool").gameObject;
        inventoryUIinstance = gameObject.GetComponent<InventoryUI>();//because the InventoryUI script is on the same object as the EquipmentManager, the GameManager
        inventory = Inventory_1.instance;//cache of inventory instance
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];//initialization of array of equipment slots
        keyBindActivatedSlots = new List<InventorySlot>();
        for (int i = 0; i < inventoryUIinstance.slots.Length; i++)//loops through all of the UI slots
        {
            if (inventoryUIinstance.slots[i].activatedByKeyBind == true)
            {
                keyBindActivatedSlots.Add(inventoryUIinstance.slots[i]);
            }
        }
    }  

    void Update()
    {
        timer -= Time.deltaTime;
        for (int i = 0; i < keyCodes.Length; i++)//loops through all of the keycodes set in the keycodes array
        {
            if (Input.GetKeyDown(keyCodes[i]))//pressed a key that is in the keycodes array
            {
                for (int j = 0; j < keyBindActivatedSlots.Count; j++)//loops through all of the slots that can be activated through key presses
                {
                    if (keyBindActivatedSlots[j].slotKeyBind == keyCodes[i] && keyBindActivatedSlots[j].item != null)//checks for the slot that has the same keycode as the keycode that was pressed
                    {
                        keyBindActivatedSlots[j].UseItem();//uses the item
                        Debug.Log("Used item of slot " + keyBindActivatedSlots[j].name);
                        Debug.Log(keyBindActivatedSlots[j].item);
                    }//Find a way to make this work when the item is null
                    //else if (keyBindActivatedSlots[j].item == null && temp == 0)
                    //{
                    //    Debug.Log(temp);
                    //    ReturnWeaponsToInventory();
                    //    Debug.Log("Slot is empty");
                    //}
                }
            }
        }
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    //Equip the weapon from the first weapon slot
        //    ActionOnKeyDown(200);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    ActionOnKeyDown(201);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    ActionOnKeyDown(202);
        //}
    }

    public void ActionOnKeyDown(int slotNumber)
    {
        if (inventoryUIinstance.slots[slotNumber].item != null)//checks if there is an item at that slot
        {
            if (playerArm.rotation.y == 0) //player facing right
            {
            Debug.Log("Player facing right. Set X value to negative");
            inventoryUIinstance.slots[slotNumber].item.x = -Mathf.Abs(inventoryUIinstance.slots[slotNumber].item.x);
            }
            else
            {
            Debug.Log("Player facing left set X value to positive");
            inventoryUIinstance.slots[slotNumber].item.x = Mathf.Abs(inventoryUIinstance.slots[slotNumber].item.x);
            }
            inventoryUIinstance.slots[slotNumber].UseItem();//Uses the item
            Inventory_1.instance.onItemChangedCallBack();
        }
        else
        {
            ReturnWeaponsToInventory();
            Debug.Log("Slot is empty");
        }
    }
    public void ReturnWeaponsToInventory()
    {
        if (playerArm != null)
        {
            for (int i = 0; i < playerArm.childCount; i++)//loops through all player arm children
            {
                for (int j = 0; j < weaponObjects.Count; j++)//loops through all available gun types
                {
                    if (playerArm.GetChild(i).name == weaponObjects[j].name + "(Clone)")//checks if we have the weapon object on the arm
                    {
                        for (int k = 0; k < currentEquipment.Length; k++)//loops through current equipment
                        {
                            for (int l = 0; l < weaponObjects.Count; l++)
                            {
                                if (currentEquipment[k] != null && currentEquipment[k].name == weaponObjects[l].name)
                                {//weapon we have equiped is also present in our list of weapons
                                    Destroy(playerArm.GetChild(i).gameObject);
                                    inventory.AddItem(currentEquipment[k]);
                                    Debug.Log("Re-added weapon to inventory");
                                    currentEquipment[k] = null;
                                    Debug.Log("emptied the equipment slot of the weapon");
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
       
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);//adds the old item back to inventory
            //i think it's safe to delete the weapon from the world at this point?
            DestroyWeapon();
        }
        if (onEquipmentChangedCallBack != null)
        {
            onEquipmentChangedCallBack(oldItem, newItem);
        }
        currentEquipment[slotIndex] = newItem;
    }

    public void InstantiateWeapon(Equipment weapon)
    {
        for (int i = 0; i < weaponPool.transform.childCount; i++)
        {                                            
            if (weapon.name == weaponPool.transform.GetChild(i).name)
            {
                mWeapon = weapon;
                //the name of the weapon picked up exists in a list of weapons
                //and it should get instantiated
                GameObject obj = weaponPool.transform.GetChild(i).gameObject;
                obj.SetActive(true);
                obj.transform.parent = playerArm;
                obj.transform.position = new Vector3(playerArmCS.weaponPositioningHelper.position.x, playerArmCS.weaponPositioningHelper.position.y, playerArmCS.weaponPositioningHelper.position.z);
                obj.transform.rotation = playerArm.rotation;
                distance = weapon.distance;
                obj.transform.localPosition = (obj.transform.localPosition - playerArm.transform.localPosition) * distance;
                //GameObject go = Instantiate(weaponObjects[i], new Vector3(playerArmCS.weaponPositioningHelper.position.x, playerArmCS.weaponPositioningHelper.position.y, playerArmCS.weaponPositioningHelper.position.z), playerArm.transform.rotation, playerArm.transform);
                //go.transform.localPosition = (go.transform.localPosition - playerArm.transform.localPosition) * distance;
                PlayerManager.instance.weapon = obj.GetComponent<FireArmBaseClass>();
            }   //instantiate the weapon as a child Gameobject of the Player's arm

            else
            {
                //the item picked up does not exist as an object or is not a weapon
                //Debug.Log("This weapon does not exist as an object or is not a weapon at weaponObjects array index " + i);
            }
        }
    }

    public void InstantiateAbility(Equipment ability)
    {
        for (int i = 0; i < abilityObjects.Length; i++)//loops through all ability prefabs in this array
        {
            if (ability.name == abilityObjects[i].name)//checks if the ability is found in this array of ability prefabs
            {
                if (timer <= 0 && playerStats.CurrentEnergy >= abilityObjects[i].GetComponent<AbilityBaseClass>().energyCost)
                {
                    Instantiate(abilityObjects[i], transform.position, Quaternion.identity);
                    timer = abilityObjects[i].GetComponent<AbilityBaseClass>().coolDown;
                }
            }
        }
    }

    void DestroyWeapon() //this works now...finally
    {
        //Debug.Log(mWeapon.name);
        for (int i = 0; i < playerArm.childCount; i++)
        {
                if (playerArm.GetChild(i).name == mWeapon.name)
                {//checks if the weapon that was equiped is a child of the player
                 //Destroy(playerArm.GetChild(i).gameObject);
                playerArm.GetChild(i).gameObject.SetActive(false);
                playerArm.GetChild(i).transform.parent = weaponPool.transform;
                }
        }
    }
}