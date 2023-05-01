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
        items.Add(itemId); // Add item ID to the set
        SaveItems(); // Save updated item set
    }

    public bool HasItem(int itemId) {
        return items.Contains(itemId); // Check if the item set contains the given ID
    }

    public void RemoveItem(int itemId) {
        items.Remove(itemId); // Remove the item ID from the set
        SaveItems();
    }

    public List<int> GetEquippedItems() {
        return items.ToList(); // Convert item set to a list and return
    }

    private void SaveItems() {
        // Serialize the item set and save it in PlayerPrefs
        string itemsJson = JsonUtility.ToJson(new SerializableItems(items));
        PlayerPrefs.SetString("Inventory", itemsJson);
        PlayerPrefs.Save();
    }

    private void LoadItems() {
        // If Inventory exists in PlayerPrefs, load and deserialize items
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
