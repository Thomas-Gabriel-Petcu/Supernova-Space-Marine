using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    #region Scene Change Detection
    private Scene loadedScene;
    GameObject player;
    PlayerManager playerManager;
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
        loadedScene = SceneManager.GetActiveScene();//returns the active scene

        if (loadedScene.name == "Station_LVL")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerManager = player.GetComponent<PlayerManager>();
        }
    }
    #endregion

    private void Start()
    {
        Debug.LogError("Developer tool on " + this + " pressing J will save the player data");
        Debug.LogError("Developer tool on " + this + " pressing L will load the player data");
    }

    private void Update()
    {
        if (loadedScene.name == "Station_LVL")
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                SavePlayer();
                SaveInventory(Inventory_1.instance);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadPlayer();
                LoadInventory();
            }
        }
    }
    public void SavePlayer()
    {
        SaveLoadSystem.SavePlayer(playerManager);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveLoadSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        player.transform.position = position;

        if (data.weaponID !=0)//means the player had a gun
        {
           GameObject weapon = Instantiate(EquipmentManager.instance.weaponObjects[data.weaponID - 1], new Vector3(playerManager.playerArmCS.weaponPositioningHelper.position.x,playerManager.playerArmCS.weaponPositioningHelper.position.y,playerManager.playerArmCS.weaponPositioningHelper.position.z), playerManager.playerArm.transform.rotation, player.transform.GetChild(0));
           weapon.transform.localPosition = (weapon.transform.localPosition - playerManager.playerArm.transform.localPosition) * weapon.GetComponent<FireArmBaseClass>().distance;
        }

    }

    public void SaveInventory(Inventory_1 inventory)
    {
        SaveLoadSystem.SaveInventory(inventory);
    }

    public void LoadInventory()
    {
        InventoryData data = SaveLoadSystem.LoadInventory();
        for (int i = 0; i < data.items.Length; i++)
        {
            if (data.items[i] == true)//checks if the boolean is true at the index, meaning there was an item at that index in the inventory
            {
                for (int j = 0; j < Inventory_1.instance.allItems.Length; j++) //loops through all of the existing items
                {
                    if (data.IDs[i] == j + 1) //checks if the ID of the item is found in the allItems array
                    {
                        Debug.Log("Item of index " + i + " with the ID " + data.IDs[i] + "was found to match allItems item at index " + j + " with ID " + Inventory_1.instance.allItems[j].ID);
                        Inventory_1.instance.items[i] = Inventory_1.instance.allItems[j]; //re-adds the item to the inventory at the correct index
                        InventoryUI.instance.UpdateUI();
                    }
                }
                
            }
        }
    }
}
