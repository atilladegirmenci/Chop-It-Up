using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public Vector3 chopAreaSize;
    public float walkSpeed;
    public float chopStrenght;

    public List<UpgradeData> upgrades;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }
    private void Start()
    {
        // initial attributes
        upgrades = new List<UpgradeData>
        {
            new UpgradeData("walkSpeed", 1, 10, 1.2f,10),
            new UpgradeData("chopStrength", 1, 15, 1.3f,11),
            new UpgradeData("chopAreaSize",1,10,3f,6),
            new UpgradeData("truckPos",1,10,3f,3),
            new UpgradeData("backpackSize",1,10,2f, 10)
        };
    }

    public UpgradeData GetUpgrade(string upgradeName)
    {
        var upgrade = upgrades.Find(upg => upg.upgradeName == upgradeName);
        if (upgrade == null)
        {
            Debug.LogWarning($"Upgrade '{upgradeName}' bulunamadi.");
        }
        return upgrade;
    }

    public void SavePlayerStats(string filePath)
    {
        PlayerStatsData playerStatsData = new PlayerStatsData(this);
        playerStatsData.upgrades = upgrades;

        string json = JsonUtility.ToJson(playerStatsData, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Player stats saved.");
    }

    public void LoadPlayerStats(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerStatsData playerStatsData = JsonUtility.FromJson<PlayerStatsData>(json);

            walkSpeed = playerStatsData.walkSpeed;
            chopStrenght = playerStatsData.chopStrength;
            chopAreaSize = playerStatsData.chopAreaSize;

            if (playerStatsData.upgrades == null)  
            {
                Debug.LogWarning("Upgrades list is null, initializing it.");
                playerStatsData.upgrades = new List<UpgradeData>();
            }

            upgrades.Clear();

            foreach (var upgrade in playerStatsData.upgrades)
            {
                upgrades.Add(upgrade);  
            }

            Debug.Log("Player stats loaded.");
        }
        else
        {
            Debug.LogWarning("Player stats file not found.");
        }
    }


}
