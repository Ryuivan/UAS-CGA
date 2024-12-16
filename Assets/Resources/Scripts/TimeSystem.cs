// =============SCRIPT TANPA BONUS TIME================

// using UnityEngine;
// using UnityEngine.UI;

// public class TimeSystem : MonoBehaviour
// {
//     public Text timerText;          // UI Text to display time
//     public Animator animator;       // Reference to the Animator component
//     public MovingRing movingRing;   // Reference to the MovingRing script
//     public GrowAndShrink growAndShrink; // Reference to the GrowAndShrink script
//     public GameObject GameOver;

//     private float timeRemaining = 60f;  // Total time for the game
//     private bool movingRingActivated = false;   // Flag to track MovingRing activation
//     private bool growAndShrinkActivated = false; // Flag to track GrowAndShrink activation
//     private bool isGameOver = false;    // Flag to check if the game is over

//     void Update()
//     {
//         // Stop further updates when the game is over
//         if (isGameOver) return;

//         // Decrease the timer
//         if (timeRemaining > 0)
//         {
//             timeRemaining -= Time.deltaTime;

//             // Clamp timeRemaining to avoid negative values
//             timeRemaining = Mathf.Max(timeRemaining, 0);

//             // Update the UI
//             UpdateTimerUI();

//             // Trigger script activations
//             CheckScriptActivations();
//         }
//         else
//         {
//             // Trigger game over logic
//             EndGame();
//         }
//     }

//     void UpdateTimerUI()
//     {
//         int seconds = Mathf.FloorToInt(timeRemaining % 60);
//         animator.SetInteger("Time", seconds); // Use SetFloat if Time is a float parameter
//         timerText.text = "Time: " + seconds.ToString();
//     }

//     void CheckScriptActivations()
//     {
//         if (timeRemaining <= 40f && !movingRingActivated)
//         {
//             // Activate MovingRing
//             if (movingRing != null)
//             {
//                 movingRing.enabled = true;
//                 Debug.Log("MovingRing script activated!");
//             }
//             movingRingActivated = true;
//         }

//         if (timeRemaining <= 20f && !growAndShrinkActivated)
//         {
//             // Activate GrowAndShrink
//             if (growAndShrink != null)
//             {
//                 growAndShrink.enabled = true;
//                 Debug.Log("GrowAndShrink script activated!");
//             }
//             growAndShrinkActivated = true;
//         }
//     }

//     void EndGame()
//     {
//         GameOver.SetActive(true);
//         Debug.Log("Game Over!");

//         // Stop the timer and ensure timeRemaining is 0
//         timeRemaining = 0;
//         UpdateTimerUI(); // Update the UI one last time to reflect the final time

//         // Set game over flag
//         isGameOver = true;

//         // Trigger a game-over animation or UI
//         animator.SetTrigger("GameOver");
//     }
// }




// ===================SCRIPT INCLUDE BONUSTIME===================
// ===================MASALAH: GAME OVER MUNCUL DI WAKTU ASLI BUKAN UPDATED BONUS TIME===================
// using UnityEngine;
// using UnityEngine.UI;

// public class TimeSystem : MonoBehaviour
// {
//     public Text timerText;          // UI Text to display time
//     public Animator animator;       // Reference to the Animator component
//     public MovingRing movingRing;   // Reference to the MovingRing script
//     public GrowAndShrink growAndShrink; // Reference to the GrowAndShrink script
//     public GameObject GameOver;

//     private float timeRemaining = 60f;  // Total time for the game
//     private bool movingRingActivated = false;   // Flag to track MovingRing activation
//     private bool growAndShrinkActivated = false; // Flag to track GrowAndShrink activation
//     private bool isGameOver = false;    // Flag to check if the game is over

//     void Update()
//     {
//         // Stop further updates when the game is over
//         if (isGameOver) return;

//         // Decrease the timer
//         if (timeRemaining > 0)
//         {
//             timeRemaining -= Time.deltaTime;

//             // Clamp timeRemaining to avoid negative values
//             timeRemaining = Mathf.Max(timeRemaining, 0);

//             // Update the UI
//             UpdateTimerUI();

//             // Trigger script activations
//             CheckScriptActivations();
//         }
//         else if (timeRemaining == 0f && !isGameOver) // Ensure game ends only when time hits exactly 0
//         {
//             // Trigger game over logic
//             EndGame();
//         }
//     }


//     void UpdateTimerUI()
//     {
//         int seconds = Mathf.FloorToInt(timeRemaining % 60);
//         animator.SetInteger("Time", seconds); // Use SetFloat if Time is a float parameter
//         timerText.text = "Time: " + seconds.ToString();
//     }

//     void CheckScriptActivations()
//     {
//         if (timeRemaining <= 40f && !movingRingActivated)
//         {
//             // Activate MovingRing
//             if (movingRing != null)
//             {
//                 movingRing.enabled = true;
//                 Debug.Log("MovingRing script activated!");
//             }
//             movingRingActivated = true;
//         }

//         if (timeRemaining <= 20f && !growAndShrinkActivated)
//         {
//             // Activate GrowAndShrink
//             if (growAndShrink != null)
//             {
//                 growAndShrink.enabled = true;
//                 Debug.Log("GrowAndShrink script activated!");
//             }
//             growAndShrinkActivated = true;
//         }
//     }

//     void EndGame()
//     {
//         GameOver.SetActive(true);
//         Debug.Log("Game Over!");

//         // Stop the timer and ensure timeRemaining is 0
//         timeRemaining = 0;
//         UpdateTimerUI(); // Update the UI one last time to reflect the final time

//         // Set game over flag
//         isGameOver = true;

//         // Trigger a game-over animation or UI
//         animator.SetTrigger("GameOver");
//     }

//     public void AddTime(float additionalTime)
//     {
//         timeRemaining += additionalTime;
//         Debug.Log("Time Added: " + additionalTime + "s. Current time: " + timeRemaining);

//         UpdateTimerUI();
//     }
// }




// ================SCRIPT SETELAH PISAH BONUSTIME================
// ================TIME SYSTEM HANYA URUS WAKTU================
// ================HOOPS URUS AKTIVASI SCRIPT MovingRing DAN GrowAndShrink================
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeSystem : MonoBehaviour
{
    public Text timerText;  // UI Text untuk menampilkan waktu
    public Animator animator;  // Animator untuk waktu animasi (jika diperlukan)
    public static TimeSystem Instance;

    public event Action OnActivateMovingRing;
    public event Action OnActivateGrowAndShrink;
    public event Action OnGameOver;  // Event untuk Game Over

    private float timeRemaining = 60f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 40f) OnActivateMovingRing?.Invoke();
            if (timeRemaining <= 20f) OnActivateGrowAndShrink?.Invoke();
        }
        else if (timeRemaining <= 0)
        {
            timeRemaining = 0; // Pastikan waktu tidak negatif
            OnGameOver?.Invoke(); // Panggil Game Over event
            enabled = false;      // Nonaktifkan TimeSystem
        }

        UpdateTimerUI();  // Panggil UpdateTimerUI setiap frame untuk memperbarui UI
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        if (animator != null) animator.SetInteger("Time", seconds); // Update animator jika ada
        timerText.text = "Time: " + seconds.ToString(); // Update UI text
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public void AddTime(float additionalTime)
    {
        timeRemaining += additionalTime;
        timeRemaining = Mathf.Min(timeRemaining, 60f); // Pastikan waktu tidak lebih dari 60 detik

        Debug.Log("Time Added: " + additionalTime + ", New Time: " + timeRemaining);

        UpdateTimerUI();  // Perbarui UI setelah menambah waktu
    }
}
