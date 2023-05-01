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
        // Initialize references and update the animator based on the items
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inventory = Game_.instance.inventory;
        UpdateAnimatorBasedOnItems();
    }

    void Update() {
        // Process input, update animator parameters, and check for interaction key press
        ProcessInput();
        UpdateAnimatorParameters();
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (Game_.instance.rule_.fsm.State == States.Game) {
                OnInteractKeyPressed?.Invoke();
            }
        }
    }

    void FixedUpdate() {
        MoveCharacter();
    }

    void ProcessInput() {
        // Get raw input from the user and normalize the movement vector
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero) {
            movement.Normalize();
        }
    }

    void UpdateAnimatorParameters() {
        // Set animator parameters based on movement
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void MoveCharacter() {
        // Update the position of the character based on the movement and game state
        if (Game_.instance.rule_.fsm.State == States.Game) {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
    public void UpdateAnimatorBasedOnItems() {
        // Update the player's animator controller based on the equipped items
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
