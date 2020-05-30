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
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;
    Animator playerAnimator;
    float intialPlayerGravity;

    bool isAlive;

    void Start() {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();
        intialPlayerGravity = playerRigidBody.gravityScale;
        isAlive = true;
    }

    void Update() {
        if (isAlive) {
            Run();
            FlipPlayerHorizontally();
            Jump();
            Climb();
            CheckPlayerDeath();
        }
    }

    private void CheckPlayerDeath() {
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard"))) {
            playerAnimator.SetTrigger("Death");
            playerRigidBody.velocity = new Vector2(-5, 10);
            isAlive = false;

            StartCoroutine(KillPlayer());
        }
    }

    private IEnumerator KillPlayer() {
        yield return new WaitForSeconds(1.5f);

        FindObjectOfType<Session>().PlayerDeath();
    }

    private void Climb() {
        bool playerIsTouchingLadder = playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));

        if (playerIsTouchingLadder) {
            playerRigidBody.gravityScale = 0;
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, controlThrow * climbVelocity);
            playerAnimator.SetBool("Climbing", PlayerIsMovingVertically());
        } else {
            playerRigidBody.gravityScale = intialPlayerGravity;
            playerAnimator.SetBool("Climbing", false);
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
        return playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool PlayerIsMovingHorizontally() {
        return Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
    }

    private bool PlayerIsMovingVertically() {
        return Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
    }
}
