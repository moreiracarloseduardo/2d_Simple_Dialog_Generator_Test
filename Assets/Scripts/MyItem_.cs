using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MyItem_ : MonoBehaviour {
    public int itemId;
    public int sellPrice;
    public string itemName;
    public Sprite itemSprite;
    public Image productImage;
    public Button sellButton;
    public TMP_Text priceText;
    public event Action<int> OnItemSold;
    private Inventory_ inventory;
    private Player_ player;


    void Start() {
        inventory = Game_.instance.inventory;
        player = GameObject.FindWithTag("Player").GetComponent<Player_>();
        sellButton.onClick.AddListener(() => SellItem());
        priceText.text = sellPrice.ToString();
        productImage.sprite = itemSprite;
    }

    public void SellItem() {
        Game_.instance.rule_.Coins += sellPrice;
        sellButton.interactable = false;
        inventory.RemoveItem(itemId);
        player.UpdateAnimatorBasedOnItems();
        OnItemSold?.Invoke(itemId);
        Destroy(gameObject);
    }
}
