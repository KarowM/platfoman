using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [SerializeField] private int playerVelocity = 4;
    [SerializeField] private int jumpVelocity = 16;
    [SerializeField] private int climbVelocity = 5;

    Rigidbody2D playerRigidBody;
    Collider2D playerCollider;
    Animator playerAnimator;

    void Start() {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update() {
        Run();
        FlipPlayerHorizontally();
        Jump();
        Climb();
        print(playerRigidBody.velocity);

    }

    private void Climb() {
        bool playerIsTouchingLadder = playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));

        if (playerIsTouchingLadder) {
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, controlThrow * climbVelocity);
            playerAnimator.SetBool("Climbing", PlayerIsMovingVertically());
        }
    }

    private void Run() {
        playerAnimator.SetBool("Running", PlayerIsMovingHorizontally());

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        playerRigidBody.velocity = new Vector2(controlThrow * playerVelocity, playerRigidBody.velocity.y);
    }

    private void Jump() {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && PlayerIsGrounded()) {
            playerRigidBody.velocity += new Vector2(0, jumpVelocity);
        }
    }

    private void FlipPlayerHorizontally() {
        if (PlayerIsMovingHorizontally()) {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1);
        }
    }
    private bool PlayerIsGrounded() {
        return playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool PlayerIsMovingHorizontally() {
        return Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
    }

    private bool PlayerIsMovingVertically() {
        return Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
    }
}
