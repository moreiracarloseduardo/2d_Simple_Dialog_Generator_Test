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
        inventory = Game_.instance.inventory;
        player = GameObject.FindWithTag("Player").GetComponent<Player_>();
        buyButton.onClick.AddListener(() => PurchaseItem());
        priceText.text = price.ToString();
        productImage.sprite = itemSprite;
    }

    public void PurchaseItem() {
        if (Game_.instance.rule_.Coins >= price) {
            Game_.instance.rule_.Coins -= price;
            buyButton.interactable = false;
            Game_.instance.inventory.AddItem(itemId);
            player.UpdateAnimatorBasedOnItems();
            AddItemToMyItems();
        }
    }
    private void AddItemToMyItems() {
        GameObject newMyItem = Instantiate(Game_.instance.ui.myItemPrefab, Game_.instance.ui.MyItemsGridObject.transform);
        MyItem_ myItem = newMyItem.GetComponent<MyItem_>();
        myItem.itemId = itemId;
        myItem.sellPrice = price / 2;
        myItem.itemName = itemName;
        myItem.itemSprite = itemSprite;

        myItem.OnItemSold += EnableBuyButton;
    }
    private void EnableBuyButton(int soldItemId) {
        if (soldItemId == itemId) {
            buyButton.interactable = true;
        }
    }

}
