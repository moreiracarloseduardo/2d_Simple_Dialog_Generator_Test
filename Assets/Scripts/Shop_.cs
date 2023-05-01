using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_ : MonoBehaviour {
    public Button closeShopButton;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject itemGrid;
    [SerializeField] private GameObject cardItemPrefab;
    [SerializeField] private List<ItemData> itemsData;
    private Player_ player;

    void Start() {
        closeShopButton.onClick.AddListener(() => CloseShop());
        player = GameObject.FindWithTag("Player").GetComponent<Player_>();
        shopPanel.SetActive(false);
        Game_.instance.ui.MyItemsPanelObject.SetActive(false);
        SetupShopItems();
    }

    void SetupShopItems() {
        foreach (ItemData itemData in itemsData) {
            GameObject newItem = Instantiate(cardItemPrefab, itemGrid.transform);
            ShopItem_ shopItem = newItem.GetComponent<ShopItem_>();
            shopItem.itemId = itemData.itemId;
            shopItem.price = itemData.price;
            shopItem.itemName = itemData.itemName;
            shopItem.itemSprite = itemData.itemSprite;
        }
    }

    void CloseShop() {
        shopPanel.SetActive(false);
        Game_.instance.ui.MyItemsPanelObject.SetActive(false);
    }

    [System.Serializable]
    public struct ItemData {
        public int itemId;
        public int price;
        public string itemName;
        public Sprite itemSprite;
    }
}