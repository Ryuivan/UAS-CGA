using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    public ScoringSystem scoringSystem;
    public TimeSystem timeSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit")) // Check if it's a fruit
        {
            string fruitType = other.gameObject.name; // Get the name of the fruit GameObject
            scoringSystem.AddScore(fruitType);

            if (fruitType == "Pineapple")
            {
                timeSystem.AddTime(5);
            }
            else
            {
                timeSystem.AddTime(3);
            }
        }
    }
}
