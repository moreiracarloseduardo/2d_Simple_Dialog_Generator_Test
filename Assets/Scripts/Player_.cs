using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : MonoBehaviour {
    public float moveSpeed = 4f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    public event Action OnInteractKeyPressed;
    private Inventory_ inventory;
    [Header("Animator Controllers")]
    public RuntimeAnimatorController playerMainController;
    public RuntimeAnimatorController playerSwordController;
    public RuntimeAnimatorController playerBlueController;
    public RuntimeAnimatorController playerSwordBlueController;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inventory = Game_.instance.inventory;
    }

    void Update() {
        ProcessInput();
        UpdateAnimatorParameters();
        if (Input.GetKeyDown(KeyCode.Return)) {
            OnInteractKeyPressed?.Invoke();
        }
    }

    void FixedUpdate() {
        MoveCharacter();
    }

    void ProcessInput() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero) {
            movement.Normalize();
        }
    }

    void UpdateAnimatorParameters() {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void MoveCharacter() {
        if (Game_.instance.rule_.fsm.State == States.Game) {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
    public void UpdateAnimatorBasedOnItems() {
        Dictionary<(int, int), RuntimeAnimatorController> itemCombinations = new Dictionary<(int, int), RuntimeAnimatorController> {
            {(1, 0), playerSwordController},
            {(3, 0), playerBlueController},
            {(1, 3), playerSwordBlueController}

        };

        int firstItemId = inventory.HasItem(1) ? 1 : inventory.HasItem(3) ? 3 : 0;
        int secondItemId = inventory.HasItem(3) && firstItemId == 1 ? 3 : 0;

        RuntimeAnimatorController animatorController;
        if (itemCombinations.TryGetValue((firstItemId, secondItemId), out animatorController)) {
            animator.runtimeAnimatorController = animatorController;
        } else {
            animator.runtimeAnimatorController = playerMainController;
        }
        List<int> equippedItems = inventory.GetEquippedItems();
        Game_.instance.ui.UpdateEquippedItemsUI(equippedItems);
    }
}
