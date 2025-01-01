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
    }

    private IEnumerator DespawnAfterTime()
    {
        isDespawning = true;
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
