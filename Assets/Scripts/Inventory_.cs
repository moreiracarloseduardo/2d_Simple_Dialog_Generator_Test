using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_ : MonoBehaviour {
    private HashSet<int> items = new HashSet<int>();

    public void AddItem(int itemId) {
        items.Add(itemId);
    }

    public bool HasItem(int itemId) {
        return items.Contains(itemId);
    }
    public void RemoveItem(int itemId) {
        items.Remove(itemId);
    }
}
