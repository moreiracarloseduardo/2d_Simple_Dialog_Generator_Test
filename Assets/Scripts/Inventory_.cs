using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory_ : MonoBehaviour {
    private HashSet<int> items = new HashSet<int>();
    void Awake() {
        LoadItems();
    }

    public void AddItem(int itemId) {
        items.Add(itemId);
        SaveItems();
    }

    public bool HasItem(int itemId) {
        return items.Contains(itemId);
    }

    public void RemoveItem(int itemId) {
        items.Remove(itemId);
        SaveItems();
    }

    public List<int> GetEquippedItems() {
        return items.ToList();
    }

    private void SaveItems() {
        string itemsJson = JsonUtility.ToJson(new SerializableItems(items));
        PlayerPrefs.SetString("Inventory", itemsJson);
        PlayerPrefs.Save();
    }

    private void LoadItems() {
        if (PlayerPrefs.HasKey("Inventory")) {
            string itemsJson = PlayerPrefs.GetString("Inventory");
            SerializableItems loadedItems = JsonUtility.FromJson<SerializableItems>(itemsJson);
            items = new HashSet<int>(loadedItems.items);
        }
    }

    [System.Serializable]
    private class SerializableItems {
        public List<int> items;

        public SerializableItems(HashSet<int> items) {
            this.items = items.ToList();
        }
    }
}
