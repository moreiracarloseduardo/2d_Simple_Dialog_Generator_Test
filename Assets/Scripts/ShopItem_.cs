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
    public Button buyButton;
    public TMP_Text priceText;

    void Start() {
        buyButton.onClick.AddListener(() => PurchaseItem());
        priceText.text = price.ToString();
    }

    public void PurchaseItem() {
        if (Game_.instance.rule_.Coins >= price) {
            Game_.instance.rule_.Coins -= price;
            buyButton.interactable = false;
            // Salvar a compra do item aqui.
        }
    }
}
