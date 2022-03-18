using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveLoadSystem
{

    public static void SavePlayer(PlayerManager playerManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(playerManager);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
            return null;
        }
    }

    public static void SaveInventory(Inventory_1 inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        InventoryData data = new InventoryData(inventory);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static InventoryData LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            InventoryData data = formatter.Deserialize(stream) as InventoryData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
            return null;
        }
    }
}
