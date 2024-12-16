using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inventoryLogAmountText;
    [SerializeField] private TextMeshProUGUI backpackLogAmountText;
    [SerializeField] private Image backpackFilledImage;
    [SerializeField] private GameObject pauseMenuPanel;
    

    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
       
    }
    //textleri update dışında güncelle!!!
    void Update()
    {
        if (InventorySystem.instance.inventory.ContainsKey(CollectableBase.collectableTypes.Log))
        {
            inventoryLogAmountText.text = ": " + InventorySystem.instance.GetItemCount(CollectableBase.collectableTypes.Log).ToString() ;
        }
        else
        {
            inventoryLogAmountText.text = ": 0";
        }


        if (BackpackSystem.instance.backpack.ContainsKey(CollectableBase.collectableTypes.Log))
        {
            backpackLogAmountText.text = BackpackSystem.instance.GetItemCount(CollectableBase.collectableTypes.Log).ToString() ;
            backpackFilledImage.fillAmount = (float)BackpackSystem.instance.GetItemCount(CollectableBase.collectableTypes.Log) / BackpackSystem.instance.backpack[CollectableBase.collectableTypes.Log].MaxCapacity;
            
        }
        else
        {
            backpackLogAmountText.text =  "";
            backpackFilledImage.fillAmount = 0;
        }

        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if(Input.GetKeyDown(KeyCode.O)) //save game
        {
            GameManager.instance.SaveGameProgress();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            GameManager.instance.LoadSceneData("currentScene");
        }


    }

}
