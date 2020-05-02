using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [SerializeField] int playerVelocity = 4;

    Rigidbody2D playerRigidBody;
    void Start() {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Run();
        FlipPlayerHorizontally();
    }

    private void Run() {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        playerRigidBody.velocity = new Vector2(controlThrow*playerVelocity, playerRigidBody.velocity.y);
    }

    private void FlipPlayerHorizontally() {
        bool playerIsMoving = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerIsMoving) {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1);
        }
    }
}
