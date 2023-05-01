using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem_ : MonoBehaviour {
    public int itemId;
    public int price;
    public string itemName;
    public Sprite itemSprite;
    public Image productImage;
    public Button buyButton;
    public TMP_Text priceText;
    public Player_ player;
    private Inventory_ inventory;

    void Start() {
        // Initialize references, set up UI components, and set up button actions
        inventory = Game_.instance.inventory;
        player = GameObject.FindWithTag("Player").GetComponent<Player_>();
        buyButton.onClick.AddListener(() => PurchaseItem());
        priceText.text = price.ToString();
        productImage.sprite = itemSprite;
    }

    public void PurchaseItem() {
        // Purchase item if the player has enough coins, update inventory and player's animator, and add the item to MyItems
        if (Game_.instance.rule_.Coins >= price) {
            Game_.instance.rule_.Coins -= price;
            buyButton.interactable = false;
            Game_.instance.inventory.AddItem(itemId);
            player.UpdateAnimatorBasedOnItems();

            MyItem_ myItem = Game_.instance.ui.AddMyItem(Game_.instance.shop.GetItemDataById(itemId));
            myItem.OnItemSold += EnableBuyButton;

        }
    }
    private void AddItemToMyItems() {
        // Instantiate a new MyItem in the MyItems grid and set up its properties, including the OnItemSold event
        GameObject newMyItem = Instantiate(Game_.instance.ui.myItemPrefab, Game_.instance.ui.MyItemsGridObject.transform);
        MyItem_ myItem = newMyItem.GetComponent<MyItem_>();
        myItem.itemId = itemId;
        myItem.sellPrice = price / 2;
        myItem.itemName = itemName;
        myItem.itemSprite = itemSprite;

        myItem.OnItemSold += EnableBuyButton;
    }
    private void EnableBuyButton(int soldItemId) {
        // Enable the buy button when the item is sold
        if (soldItemId == itemId) {
            buyButton.interactable = true;
        }
    }


}
