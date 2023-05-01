using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Ui_ : MonoBehaviour {
    [Header("Dialogue Objects References")]
    public GameObject promptTextPrefab;
    public GameObject dialoguePanelObject;
    public TextMeshProUGUI dialogueText;
    public Image avatar;
    [Header("Shop")]
    public GameObject shopObject;
    [Header("Coins")]
    public GameObject coinsObject;
    public TextMeshProUGUI coinsTotalText;
    [Header("My Items Panel")]
    public GameObject MyItemsPanelObject;
    public GameObject MyItemsGridObject;
    public GameObject myItemPrefab;
    [Header("Equipped Items")]
    public Image firstItemSlot;
    public Image secondItemSlot;
    public GameObject EquippedItemsObject;
    [Header("Start")]
    public GameObject startUi;
    [Header("Pause")]
    public GameObject pauseUi;
    public Button quitButton;

    void Start() {
        if (Game_.instance.rule_.fsm.State == States.Start) {
            startUi.SetActive(true);
        }
        LoadMyItems();
        pauseUi.SetActive(false);
        quitButton.onClick.AddListener(() => Application.Quit());
    }
    public void UpdateEquippedItemsUI(List<int> equippedItems) {
        // Update the equipped items UI based on the equippedItems list
        if (equippedItems.Count >= 1) {
            firstItemSlot.sprite = GetItemSpriteById(equippedItems[0]);
            firstItemSlot.enabled = true;
            firstItemSlot.gameObject.SetActive(true);
        } else {
            firstItemSlot.enabled = false;
            firstItemSlot.gameObject.SetActive(false);
        }

        if (equippedItems.Count >= 2) {
            secondItemSlot.sprite = GetItemSpriteById(equippedItems[1]);
            secondItemSlot.enabled = true;
            secondItemSlot.gameObject.SetActive(true);
        } else {
            secondItemSlot.enabled = false;
            secondItemSlot.gameObject.SetActive(false);
        }
    }
    public Sprite GetItemSpriteById(int itemId) {
        Shop_.ItemData itemData = Game_.instance.shop.GetItemDataById(itemId);

        if (itemData.itemSprite != null) {
            return itemData.itemSprite;
        }

        return null;
    }
    private void LoadMyItems() {
        // Load the player's items and display them in the UI
        List<int> myItems = Game_.instance.inventory.GetEquippedItems();
        foreach (int itemId in myItems) {
            Shop_.ItemData itemData = Game_.instance.shop.GetItemDataById(itemId);
            if (itemData.itemId != 0) {
                AddMyItem(itemData);
            }
        }
    }

    public MyItem_ AddMyItem(Shop_.ItemData itemData) {
        // Instantiate a new item and add it to the UI
        GameObject newMyItem = Instantiate(myItemPrefab, MyItemsGridObject.transform);
        MyItem_ myItem = newMyItem.GetComponent<MyItem_>();
        myItem.itemId = itemData.itemId;
        myItem.sellPrice = itemData.price / 2;
        myItem.itemName = itemData.itemName;
        myItem.itemSprite = itemData.itemSprite;

        return myItem;
    }
}
