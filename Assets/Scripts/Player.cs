using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [SerializeField] int playerVelocity = 4;

    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    void Start() {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update() {
        Run();
        FlipPlayerHorizontally();
    }

    private void Run() {
        playerAnimator.SetBool("Running", PlayerIsMoving());

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        playerRigidBody.velocity = new Vector2(controlThrow * playerVelocity, playerRigidBody.velocity.y);
    }
    private void FlipPlayerHorizontally() {
        if (PlayerIsMoving()) {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1);
        }
    }

    private bool PlayerIsMoving() {
        return Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
    }
}
