using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int CurrentAmount; 
    public int MaxCapacity;   

    public ItemData(int amount)
    {
        CurrentAmount += amount ;
        MaxCapacity = 5;
    }
}
public class BackpackSystem : MonoBehaviour
{
    public Dictionary<CollectableBase.collectableTypes, ItemData> backpack = new Dictionary<CollectableBase.collectableTypes, ItemData>();

    public static BackpackSystem instance;

   
    private void Awake()
    {
        instance = this;    
    }
    public bool AddItem(CollectableBase.collectableTypes itemName, int amount)
    {
        if (backpack.ContainsKey(itemName))
        {
            var item = backpack[itemName];
            if (item.CurrentAmount < item.MaxCapacity && item.CurrentAmount + amount <= item.MaxCapacity)
            {
                item.CurrentAmount+= amount;
                Debug.Log($"{itemName} eklendi. su anki miktar: {item.CurrentAmount}");
                return true;
            }
            else
            {
                Debug.Log($"{itemName} için maksimum kapasiteye ulasildi.");
                return false;
            }
        }
        else
        {
            backpack.Add(itemName, new ItemData(amount));
            Debug.Log($"{itemName} çantaya yeni eklendi. şu anki miktarı: {backpack[itemName].CurrentAmount}");
            return true;
        }
    }

    public void TransferToInv()
    {
        foreach  (var pair in backpack)
        {
            if(backpack.ContainsKey(pair.Key))
            {
                InventorySystem.instance.AddItem(pair.Key, pair.Value.CurrentAmount);
                pair.Value.CurrentAmount = 0;
               
            }
        }
       
    }
    public void UpdateMaxCapacity()
    {
       
        foreach (var pair in backpack)
        {
            if (backpack.ContainsKey(pair.Key))
            {
                pair.Value.MaxCapacity *= 2;

            }
        }
    }

    public int GetItemCount(CollectableBase.collectableTypes itemName)
    {
        return backpack.ContainsKey(itemName) ? backpack[itemName].CurrentAmount : 0;
    }

    public BackpackData GetBackpackData()
    {
        BackpackData data = new BackpackData();
        foreach (var item in backpack)
        {
            data.backpack.Add(new BackpackItem(item.Key, item.Value));
        }
        return data;
    }

    public void LoadBackpackData(BackpackData data)
    {
        backpack.Clear();
        foreach (var item in data.backpack)
        {
            CollectableBase.collectableTypes type = (CollectableBase.collectableTypes)System.Enum.Parse(typeof(CollectableBase.collectableTypes), item.collectableType);

            if (item.itemData == null)
            {
                Debug.LogWarning("ItemData is null for " + item.collectableType + ", creating a new one.");
                item.itemData = new ItemData(0); 
            }

            if (!backpack.ContainsKey(type))
            {
                backpack[type] = new ItemData(item.itemData.CurrentAmount); 
            }

            backpack[type] = item.itemData;
        }
    }

    public void SaveBackpack(string filePath)
    {
        BackpackData data = GetBackpackData();
        string json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log($"Backpack saved to {filePath}");
    }

    public void LoadBackpack(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            BackpackData data = JsonUtility.FromJson<BackpackData>(json);
            LoadBackpackData(data);
            Debug.Log($"backpack loaded from {filePath}");
        }
        else
        {
            Debug.LogWarning($"Save file not found at {filePath}");
        }
    }
}
