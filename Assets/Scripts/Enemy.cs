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

    private void OnTriggerExit2D(Collider2D collision) {
        enemyRigidbody.velocity = new Vector2(-enemyRigidbody.velocity.x, 0);
        transform.localScale = new Vector2(Mathf.Sign(enemyRigidbody.velocity.x), 1);
    }
}
