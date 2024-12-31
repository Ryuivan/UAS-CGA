using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeSystem : MonoBehaviour
{
    public Text timerText;          // UI Text to display time
    public Text highScoreText;      // UI Text to display high score
    public Text currentScoreText;   // UI Text to display current score
    public Text countdownMenuText; // UI Text to display countdown to menu

    public ScoringSystem scoringSystem; // Reference to the ScoringSystem script
    public Animator animator;       // Reference to the Animator component
    public MovingRing movingRing;   // Reference to the MovingRing script
    public GrowAndShrink growAndShrink; // Reference to the GrowAndShrink script
    public MusicPlayer musicPlayer;
    public GameObject GameOver;

    private float timeRemaining = 60f;  // Total time for the game
    private bool movingRingActivated = false;   // Flag to track MovingRing activation
    private bool growAndShrinkActivated = false; // Flag to track GrowAndShrink activation
    private bool isGameOver = false;    // Flag to check if the game is over
    private bool isCountingDown = false; // Flag to track countdown state

    [SerializeField]
    private AudioClip countdown;

    void Awake()
    {
    
    }

    void Update()
    {
        if (isGameOver)
        {
            // Destroy all Fruit objects in the scene
            GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
            foreach (GameObject fruit in fruits)
            {
                Destroy(fruit);
            }
            return;
        }

        // Decrease the timer
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            // Trigger script activations
            CheckScriptActivations();

            // Update the UI
            UpdateTimerUI();
        }
        else
        {
            // Trigger game over logic
            EndGame();
        }
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.FloorToInt(timeRemaining);
        timerText.text = seconds.ToString();
    }

    void CheckScriptActivations()
    {
        if (timeRemaining <= 40f && !movingRingActivated)
        {
            if (movingRing != null)
            {
                movingRing.enabled = true;
            }
            movingRingActivated = true;
        }

        if (timeRemaining <= 20f && !growAndShrinkActivated)
        {
            if (growAndShrink != null)
            {
                growAndShrink.enabled = true;
            }
            growAndShrinkActivated = true;
        }

        if (timeRemaining <= 10f && countdown != null)
        {
            musicPlayer.PlaySFXCountdown(countdown);
        }
    }

    void EndGame()
    {
        musicPlayer.StopSFXCountdown();
        currentScoreText.text = "Current Score: " + scoringSystem.getScore();

        if (scoringSystem.getScore() > scoringSystem.ReadHighScore())
        {
            highScoreText.text = "High Score: " + scoringSystem.getScore();
            scoringSystem.SaveHighScore();
            musicPlayer.PlayHappyMusic();
        }
        else
        {
            highScoreText.text = "High Score: " + scoringSystem.ReadHighScore();
            musicPlayer.PlaySadMusic();
        }

        GameOver.SetActive(true);

        // Stop the timer and ensure timeRemaining is 0
        timeRemaining = 0;
        UpdateTimerUI();

        // Set game over flag
        isGameOver = true;

        // Disable all scripts
        if (movingRing != null)
        {
            movingRing.enabled = false;
            movingRingActivated = false;
        }
        if (growAndShrink != null)
        {
            growAndShrink.enabled = false;
            growAndShrinkActivated = false;
        }
        if (animator != null)
        {
            animator.enabled = false;
        }

        // Start countdown to menu
        if (!isCountingDown)
        {
            StartCoroutine(CountdownToMainMenu());
        }
    }

    private IEnumerator CountdownToMainMenu()
    {
        isCountingDown = true;
        float countdownTime = 15f;

        while (countdownTime > 0)
        {
            countdownMenuText.text = "Returning to Main Menu in " + Mathf.CeilToInt(countdownTime) + " seconds...";
            countdownTime -= Time.deltaTime;
            yield return null;
        }

        // Automatically navigate to Main Menu
        LoadMainMenu();
    }

    public void AddTime(int timeToAdd)
    {
        timeRemaining += timeToAdd;
        CheckScriptActivations();
    }

    public void OnBackToMainMenuButton()
    {
        StopAllCoroutines();
        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        Debug.Log("Navigating to Main Menu...");
        // Implement scene loading logic here
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
