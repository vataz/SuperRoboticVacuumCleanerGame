using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour {
    int score;
    int maxScore;

    public Text scoreText;

    public LevelManager lm;
    void Start() {
        UpdateScoreText();
    }

    public void AddPoints() {
        score++;
        UpdateScoreText();
        CheckForCompletion();
    }

    public void UpdateScoreText() {
        scoreText.text = score+" / "+maxScore;
    }

    public void SetMaxScore(int newMaxScore) {
        maxScore = newMaxScore;
    }

    public void CheckForCompletion() {
        if (score == maxScore) {
            lm.EndLevel();
        }
    }
}
