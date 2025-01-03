using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class ScoringSystem : MonoBehaviour
{
    private string highScoreFilePath = Path.Combine(Application.dataPath, "../highscore.txt");

    public void SaveHighScore()
    {
        System.IO.File.WriteAllText(highScoreFilePath, score.ToString());
    }

    public int ReadHighScore()
    {
        if (System.IO.File.Exists(highScoreFilePath))
        {
            string scoreString = System.IO.File.ReadAllText(highScoreFilePath);
            if (int.TryParse(scoreString, out int highScore))
            {
                return highScore;
            }
        }
        return 0;
    }
    public Text scoreText;
    private int score = 0;

    private Dictionary<string, int> fruitScores = new Dictionary<string, int>
    {
        {"Apple", 22},
        {"Banana", 15},
        {"Orange", 20},
        {"Pear", 25},
        {"Pineapple", 35}
    };

    public int getScore()
    {
        return score;
    }

    public void AddScore(string fruitType)
    {
        fruitType = fruitType.Replace("(Clone)", "").Trim();

        if (fruitScores.ContainsKey(fruitType))
        {
            score += fruitScores[fruitType];
            // Debug.Log("Added score for: " + fruitType + ". Current score: " + score);
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