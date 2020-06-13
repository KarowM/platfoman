using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Session : MonoBehaviour {

    [SerializeField] Text lives;
    [SerializeField] Text score;

    int numOfLives = 3;
    int currentScore = 0;
    private void Awake() {
        if (FindObjectsOfType<Session>().Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start() {
        RefreshHud();
    }

    public void PlayerDeath() {
        if (numOfLives > 0) {
            numOfLives--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            RefreshHud();
        } else {
            RestartSession();
        }
    }

    public void addToScore(int amount) {
        currentScore += amount;
        RefreshHud();
    }
    private void RefreshHud() {
        lives.text = "Lives: " + numOfLives;
        score.text = "Score: " + currentScore;
    }

    private void RestartSession() {
        // TODO: go to failed scene
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
