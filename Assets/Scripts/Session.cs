using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Session : MonoBehaviour {

    int numOfLives = 3;
    private void Awake() {
        if (FindObjectsOfType<Session>().Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start() {

    }

    public void PlayerDeath() {
        if (numOfLives > 0) {
            numOfLives--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else {
            RestartSession();
        }
    }

    private void RestartSession() {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
