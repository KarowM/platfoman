using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [SerializeField] private int playerVelocity = 4;
    [SerializeField] private int jumpVelocity = 16;


    Rigidbody2D playerRigidBody;
    Collider2D playerCollider;
    Animator playerAnimator;

    void Start() {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update() {
        print(playerRigidBody.velocity.y);
        Run();
        FlipPlayerHorizontally();
        Jump();
    }

    private void Run() {
        playerAnimator.SetBool("Running", PlayerIsMoving());

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        playerRigidBody.velocity = new Vector2(controlThrow * playerVelocity, playerRigidBody.velocity.y);
    }

    private void Jump() {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && PlayerIsGrounded()) {
            playerRigidBody.velocity += new Vector2(0, jumpVelocity);
        }
    }

    private void FlipPlayerHorizontally() {
        if (PlayerIsMoving()) {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1);
        }
    }
    private bool PlayerIsGrounded() {
        return playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool PlayerIsMoving() {
        return Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
    }
}
