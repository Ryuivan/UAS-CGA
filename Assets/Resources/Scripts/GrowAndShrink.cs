using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowAndShrink : MonoBehaviour
{
    public float growTime = 5f;
    public float maxSize = 2f;
    public float minSize = 1f; // Minimum size for shrinking

    private bool isGrowing = true; // Tracks whether the object is currently growing

    void Start()
    {
        StartCoroutine(GrowAndShrinkCoroutine());
    }

    private IEnumerator GrowAndShrinkCoroutine()
    {
        while (true)
        {
            Vector3 startScale = transform.localScale;
            Vector3 targetScale = isGrowing 
                ? new Vector3(maxSize, maxSize, maxSize) 
                : new Vector3(minSize, minSize, minSize);

            float timer = 0f;

            while (timer < growTime)
            {
                transform.localScale = Vector3.Lerp(startScale, targetScale, timer / growTime);
                timer += Time.deltaTime;
                yield return null;
            }

            transform.localScale = targetScale; // Ensure the final scale is exact
            isGrowing = !isGrowing; // Toggle between growing and shrinking
        }
    }
}
