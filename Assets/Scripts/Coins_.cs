using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins_ : MonoBehaviour {
    [SerializeField] private int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            CollectCoin();
        }
    }

    private void CollectCoin() {
        Game_.instance.rule_.Coins += coinValue;
        Destroy(gameObject);
    }
}