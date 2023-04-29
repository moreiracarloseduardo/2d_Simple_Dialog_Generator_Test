using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : MonoBehaviour {
    public float moveSpeed = 4f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        ProcessInput();
        UpdateAnimatorParameters();
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
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
