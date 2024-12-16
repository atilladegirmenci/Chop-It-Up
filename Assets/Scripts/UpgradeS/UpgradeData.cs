using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeData 
{
    public static UpgradeData instance;

    public string upgradeName;
    public int level;
    public int maxlevel;
    public int cost;
    public float costMultiplier;

    
    public UpgradeData(string name, int startLevel, int startCost, float multiplier, int maxlvl)
    {
        upgradeName = name;
        level = startLevel;
        cost = startCost;
        costMultiplier = multiplier;
        maxlevel = maxlvl;
    }

   
    public void IncreaseCost()
    {
        cost = Convert.ToInt32(cost* costMultiplier);
    }
    public void IncreaseLevel()
    {
        level++;
    }
}

