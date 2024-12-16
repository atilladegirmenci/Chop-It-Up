using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem instance;
    [SerializeField] private List<GameObject> axeList;

    private void Awake()
    {
        instance = this;
    }
   
    public void UpgradeSpeed()
    {
        UpgradeData speedUpgrade = PlayerStats.instance.GetUpgrade("walkSpeed");

        if (speedUpgrade.level < speedUpgrade.maxlevel)
        {
            if (InventorySystem.instance.HasEnoughAmount(speedUpgrade.cost))
            {
                InventorySystem.instance.RemoveItem(CollectableBase.collectableTypes.Log, speedUpgrade.cost);
                speedUpgrade.IncreaseLevel();
                speedUpgrade.IncreaseCost();

                PlayerStats.instance.walkSpeed += 0.2f; //walk speed updated
            }
            else
            {
                Debug.Log("not enough materials");
            }
        }
        else { Debug.Log("max level has reached"); }
    }
    public void UpgradeChopStrenght()
    {
        UpgradeData strengthUpgrade = PlayerStats.instance.GetUpgrade("chopStrength");

        if (strengthUpgrade.level < strengthUpgrade.maxlevel)
        {

            if (InventorySystem.instance.HasEnoughAmount(strengthUpgrade.cost))
            {
                InventorySystem.instance.RemoveItem(CollectableBase.collectableTypes.Log, strengthUpgrade.cost);
                strengthUpgrade.IncreaseLevel();
                strengthUpgrade.IncreaseCost();


                PlayerStats.instance.chopStrenght += 0.2f; //chop strenght updated


                ChangeAxe(strengthUpgrade.level);
            }
            else
            {
                Debug.Log("not enough materials");
            }
        }
        else { Debug.Log("max level has reached"); }
    }
    public void UpgradeChopAreaSize()
    {
        UpgradeData areaUpgrade = PlayerStats.instance.GetUpgrade("chopAreaSize");

        if (areaUpgrade.level < areaUpgrade.maxlevel)
        {
            if (InventorySystem.instance.HasEnoughAmount(areaUpgrade.cost))
            {
                InventorySystem.instance.RemoveItem(CollectableBase.collectableTypes.Log, areaUpgrade.cost);
                areaUpgrade.IncreaseLevel();
                areaUpgrade.IncreaseCost();

                PlayerStats.instance.chopAreaSize = PlayerStats.instance.chopAreaSize + new Vector3(0.1f, 0, 0.1f);

            }
            else
            {
                Debug.Log("not enough materials");
            }
        }
        else { Debug.Log("max level has reached"); }
    }
    public void UpgradeTruckPos()
    {
        UpgradeData posUpgrade = PlayerStats.instance.GetUpgrade("truckPos");

        if (posUpgrade.level < posUpgrade.maxlevel)
        {
            if (InventorySystem.instance.HasEnoughAmount(posUpgrade.cost))
            {
                InventorySystem.instance.RemoveItem(CollectableBase.collectableTypes.Log, posUpgrade.cost);
                posUpgrade.IncreaseLevel();
                posUpgrade.IncreaseCost();

                StartCoroutine(UpgradeArea.instance.movePos());
            }
            else
            {
                Debug.Log("not enough materials");
            }
        }
        else { Debug.Log("max level has reached"); }
    }
    public void UpgradeBackpackSize()
    {
        UpgradeData backpackUprade = PlayerStats.instance.GetUpgrade("backpackSize");

        if (backpackUprade.level < backpackUprade.maxlevel)
        {
            if (InventorySystem.instance.HasEnoughAmount(backpackUprade.cost))
            {
                InventorySystem.instance.RemoveItem(CollectableBase.collectableTypes.Log, backpackUprade.cost);
                backpackUprade.IncreaseLevel();
                backpackUprade.IncreaseCost();

                BackpackSystem.instance.UpdateMaxCapacity();
            }
            else
            {
                Debug.Log("not enough materials");
            }
        }
        else { Debug.Log("max level has reached"); }
    }

    private void ChangeAxe(int strengthLevel)
    {
        int newIndex = (strengthLevel -1) / 2; // update axe model every 2 level

        if (newIndex >= axeList.Count)
        {
            newIndex = axeList.Count - 1;
        }

        for (int i = 0; i < axeList.Count; i++)
        {
            if (i == newIndex)
            {
                axeList[i].SetActive(true);
            }
            else 
            {
                axeList[i].SetActive(false);
            }
            
        }


    }
}
