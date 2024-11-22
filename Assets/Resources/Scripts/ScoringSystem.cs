using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    private Dictionary<string, int> fruitScores = new Dictionary<string, int>
    {
        {"Apple", 10},
        {"Banana", 20},
        {"Orange", 15},
        {"Pear", 25},
        {"Pineapple", 30}
    };

    public void AddScore(string fruitType)
    {
        fruitType = fruitType.Replace("(Clone)", "").Trim();

        if (fruitScores.ContainsKey(fruitType))
        {
            score += fruitScores[fruitType];
            Debug.Log("Added score for: " + fruitType + ". Current score: " + score);
            UpdateScoreUI();
        }
        else
        {
            Debug.LogWarning("Fruit type not found: " + fruitType);
        }
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}