using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRing : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 0.5f;
    int direction = 1;

    void Start()
    {
        // Check and log if any required Transform is missing
        if (platform == null)
        {
            Debug.LogWarning("MovingRing: Platform not assigned. Using self as platform.");
            platform = transform; // Defaults to the GameObject this script is attached to
        }
        else
        {
            Debug.Log("Platform assigned: " + platform.name);
        }

        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("MovingRing: StartPoint or EndPoint is not assigned!");
        }
        else
        {
            Debug.Log("StartPoint and EndPoint assigned.");
        }

        speed = 0.4f; // Set the initial speed programmatically
    }

    void Update()
    {
        platform = transform.GetChild(0);
        startPoint = transform.GetChild(1);
        endPoint = transform.GetChild(2);
        
        // Ensure that all required references are not null
        if (platform == null || startPoint == null || endPoint == null)
        {
            Debug.LogError("MovingRing: One or more required Transforms are not assigned!");
            return;
        }

        // Handle platform movement
        Vector2 target = currentMovementTarget();
        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)platform.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *= -1;  // Reverse direction once the platform reaches the target
        }
    }

    // Returns the target position for the platform based on direction
    Vector2 currentMovementTarget()
    {
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("MovingRing: StartPoint or EndPoint is missing. Cannot determine movement target.");
            return platform.position;  // Return current position to avoid further errors
        }

        if (direction == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the platform's movement path in the scene view
        if (platform != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(platform.position, startPoint.position);
            Gizmos.DrawLine(platform.position, endPoint.position);
        }
    }
}
