using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private bool isThrown = false;
    private float despawnTime;
    private bool isDespawning = false;

    public void SetDespawnTime(float time)
    {
        despawnTime = time;
    }

    private void Update()
    {
        if (!isThrown && transform.position.y > 0.1f)
        {
            isThrown = true;
        }

        if (isThrown && !isDespawning)
        {
            StartCoroutine(DespawnAfterTime());
        }

        if (!IsInView())
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DespawnAfterTime()
    {
        isDespawning = true;
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    private bool IsInView()
    {
        if (Camera.main == null) return false;
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.z > 0;
    }
}
