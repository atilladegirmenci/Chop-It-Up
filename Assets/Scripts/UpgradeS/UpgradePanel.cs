using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [Header("UPGRADE COSTS")]
    [SerializeField] private TextMeshProUGUI walkSpeedUpgradeAmount;
    [SerializeField] private TextMeshProUGUI chopStrenghtUpgradeAmount;
    [SerializeField] private TextMeshProUGUI chopAreaSizeUpgradeAmount;
    [SerializeField] private TextMeshProUGUI truckPosUpgradeAmount;
    [SerializeField] private TextMeshProUGUI backpackSizeUpgradeAmount;
    [Header("UPGRADE LEVELS")]
    [SerializeField] private TextMeshProUGUI walkspeedUpgradelvl;
    [SerializeField] private TextMeshProUGUI chopStrengthUpgradelvl;
    [SerializeField] private TextMeshProUGUI chopAreaSizeUpgradelvl;
    [SerializeField] private TextMeshProUGUI truckPosUpgradelvl;
    [SerializeField] private TextMeshProUGUI backpackSizeUpgradelvl;

    private void OnEnable()
    {
        walkSpeedUpgradeAmount.text = PlayerStats.instance.GetUpgrade("walkSpeed").cost.ToString();
        chopStrenghtUpgradeAmount.text = PlayerStats.instance.GetUpgrade("chopStrength").cost.ToString();
        chopAreaSizeUpgradeAmount.text = PlayerStats.instance.GetUpgrade("chopAreaSize").cost.ToString();
        backpackSizeUpgradeAmount.text = PlayerStats.instance.GetUpgrade("backpackSize").cost.ToString();
        truckPosUpgradeAmount.text = PlayerStats.instance.GetUpgrade("truckPos").cost.ToString();

        walkspeedUpgradelvl.text = PlayerStats.instance.GetUpgrade("walkSpeed").level.ToString();
        chopStrengthUpgradelvl.text = PlayerStats.instance.GetUpgrade("chopStrength").level.ToString();
        chopAreaSizeUpgradelvl.text = PlayerStats.instance.GetUpgrade("chopAreaSize").level.ToString();
        backpackSizeUpgradelvl.text = PlayerStats.instance.GetUpgrade("backpackSize").level.ToString();
        truckPosUpgradelvl.text = PlayerStats.instance.GetUpgrade("truckPos").level.ToString();

    }


    public void SpeedUpgrade()
    {
        UpgradeSystem.instance.UpgradeSpeed();
        walkSpeedUpgradeAmount.text = PlayerStats.instance.GetUpgrade("walkSpeed").cost.ToString();
        walkspeedUpgradelvl.text = setUpgradeLevelText(PlayerStats.instance.GetUpgrade("walkSpeed"));
        GameManager.instance.SaveGameProgress();
    }
    public void ChopStrenghtUpgrade()
    {
        UpgradeSystem.instance.UpgradeChopStrenght();
        chopStrenghtUpgradeAmount.text = PlayerStats.instance.GetUpgrade("chopStrength").cost.ToString();
        chopStrengthUpgradelvl.text = setUpgradeLevelText(PlayerStats.instance.GetUpgrade("chopSrength"));
        GameManager.instance.SaveGameProgress();
    }
    public void ChopAreaSizeUpgrage()
    {
        UpgradeSystem.instance.UpgradeChopAreaSize();
        chopAreaSizeUpgradeAmount.text = PlayerStats.instance.GetUpgrade("chopAreaSize").cost.ToString();
        chopAreaSizeUpgradelvl.text = setUpgradeLevelText(PlayerStats.instance.GetUpgrade("chopAreaSize"));
        GameManager.instance.SaveGameProgress();
    }
    public void TruckPosUpgrade()
    {
        UpgradeSystem.instance.UpgradeTruckPos();
        truckPosUpgradeAmount.text = PlayerStats.instance.GetUpgrade("truckPos").cost.ToString();
        truckPosUpgradelvl.text = setUpgradeLevelText(PlayerStats.instance.GetUpgrade("truckPos"));
        GameManager.instance.SaveGameProgress();

    }
    public void BackpackSizeUpgrade()
    {
        UpgradeSystem.instance.UpgradeBackpackSize();
        backpackSizeUpgradeAmount.text = PlayerStats.instance.GetUpgrade("backpackSize").cost.ToString();
        backpackSizeUpgradelvl.text = setUpgradeLevelText(PlayerStats.instance.GetUpgrade("backpackSize"));
        GameManager.instance.SaveGameProgress();
    }

    public string setUpgradeLevelText(UpgradeData upgrade)
    {
        if(upgrade.level == upgrade.maxlevel)
        {
           return "MAX";
        }
        else
        {
            return upgrade.level.ToString();
        }
    }
    public void turnOffPanel()
    {
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

  
}
