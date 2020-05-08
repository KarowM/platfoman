using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int walkSpeed = 1;

    Rigidbody2D enemyRigidbody;
    void Start() {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyRigidbody.velocity = new Vector2(walkSpeed, 0);
    }

    void Update() {
        FlipPlayerHorizontally();
    }

    private void FlipPlayerHorizontally() {
        transform.localScale = new Vector2(Mathf.Sign(enemyRigidbody.velocity.x), 1);
    }
}
