using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    public ScoringSystem scoringSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit")) // Check if it's a fruit
        {
            string fruitType = other.gameObject.name; // Get the name of the fruit GameObject
            scoringSystem.AddScore(fruitType);

            // Optionally, destroy the fruit after scoring
            Destroy(other.gameObject);
        }
    }
}
