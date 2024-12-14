using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private bool isThrown = false;
    private float despawnTime;

    public void SetDespawnTime(float time)
    {
        despawnTime = time;
    }

    void Update()
    {
        if (transform.position.y > 0.1f)
        {
            isThrown = true;
        }

        if (isThrown)
        {
            StartCoroutine(DespawnAfterTime());
        }
    }

    private IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
