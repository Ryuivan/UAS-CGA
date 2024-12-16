using UnityEngine;

public class Hoops : MonoBehaviour
{
    public MovingRing movingRing;
    public GrowAndShrink growAndShrink;
    public GameObject gameOverUI; // Referensi ke UI Game Over

    void Start()
    {
        TimeSystem.Instance.OnActivateMovingRing += ActivateMovingRing;
        TimeSystem.Instance.OnActivateGrowAndShrink += ActivateGrowAndShrink;
        TimeSystem.Instance.OnGameOver += ShowGameOverUI; // Tambahkan Game Over listener
    }

    void ActivateMovingRing()
    {
        if (movingRing != null && !movingRing.enabled)
        {
            movingRing.enabled = true;
            Debug.Log("MovingRing Activated!");
        }
    }

    void ActivateGrowAndShrink()
    {
        if (growAndShrink != null && !growAndShrink.enabled)
        {
            growAndShrink.enabled = true;
            Debug.Log("GrowAndShrink Activated!");
        }
    }

    void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Aktifkan Game Over UI
            Debug.Log("Game Over!");
        }
    }
}
