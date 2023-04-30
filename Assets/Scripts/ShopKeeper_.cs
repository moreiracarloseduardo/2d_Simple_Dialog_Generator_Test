using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopKeeper_ : MonoBehaviour {
    public float interactionDistance = 1.5f;
    private TextMeshProUGUI promptText;
    private Transform playerTransform;

    void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        promptText = Game_.instance.ui.promptTextObject.GetComponentInChildren<TextMeshProUGUI>();
        promptText.enabled = false;
    }

    void Update() {
        CheckForPlayerInteraction();
    }

    void CheckForPlayerInteraction() {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionDistance) {
            promptText.enabled = true;

            if (Input.GetKeyDown(KeyCode.Return)) {
                // Interact with the ShopKeeper
                Debug.Log("Interacting with ShopKeeper...");
            }
        } else {
            promptText.enabled = false;
        }
    }
}
