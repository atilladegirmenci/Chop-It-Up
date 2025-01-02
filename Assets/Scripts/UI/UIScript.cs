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

    [Header("NOTIFICATION")]
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private float notificDisplayDuration = 2f;
    [SerializeField] public float moveDuration;
    [SerializeField] public float offscreenPositionX;
    [SerializeField] public float onscreenPositionX;
    private Coroutine currentCoroutine;
    
    static public UIScript instance;

    private void Awake()
    {
        instance = this;
    }
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
            int logAmount = BackpackSystem.instance.GetItemCount(CollectableBase.collectableTypes.Log);
            int maxCapacity = BackpackSystem.instance.backpack[CollectableBase.collectableTypes.Log].MaxCapacity;

            backpackLogAmountText.text = logAmount.ToString();
            backpackFilledImage.fillAmount = (float)logAmount / maxCapacity;

            backpackFilledImage.color = (logAmount == maxCapacity) ? new Color(0.75f, 0f, 0f) : Color.black;
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
       
    }
    public void ShowNotification(string message)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(SlideNotification(message));
    }

    private IEnumerator SlideNotification(string message)
    {
        notificationText.text = message;

        float elapsedTime = 0f;
        Vector2 startPosition = new Vector2(offscreenPositionX, notificationText.rectTransform.anchoredPosition.y);
        Vector2 targetPosition = new Vector2(onscreenPositionX, notificationText.rectTransform.anchoredPosition.y);
        
        //slide in
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            notificationText.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            yield return null;
        }

        //wait
        yield return new WaitForSeconds(notificDisplayDuration);

        // slide out
        elapsedTime = 0f;
        startPosition = notificationText.rectTransform.anchoredPosition;
        targetPosition = new Vector2(offscreenPositionX, notificationText.rectTransform.anchoredPosition.y);

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            notificationText.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            yield return null;
        }

        notificationText.text = "";

        currentCoroutine = null;
       
    }
}
