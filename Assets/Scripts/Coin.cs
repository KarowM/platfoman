﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField] AudioClip coinPickup;

    private void OnTriggerEnter2D(Collider2D collision) {
        AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position);
        FindObjectOfType<Session>().addToScore(100);
        Destroy(gameObject);
    }
}
