using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAdjuster : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Image DragBar;
    public List<Rigidbody> activeFruits = new List<Rigidbody>();

    public float dragChangeRate = 2f;
    public float maxDrag = 0f;
    public float minDrag = 0f;

    private bool isHolding = false;
    private float holdTime;

    void Start()
    {
        if (DragBar != null)
        {
            DragBar.fillAmount = 0f;
        }
    }

    void Update()
    {
        if (!isHolding && DragBar != null && activeFruits.Count > 0)
        {
            Rigidbody rb = activeFruits[0];
            if (rb != null)
            {
                DragBar.fillAmount = Mathf.InverseLerp(minDrag, maxDrag, rb.drag); 
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        holdTime = 0f;

        if (DragBar != null)
        {
            DragBar.fillAmount = 0f;
        }

        StartCoroutine(StartAdjustingDrag());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;

        foreach (Rigidbody rb in activeFruits)
        {
            if (rb != null)
            {
                rb.drag = Mathf.Clamp(rb.drag + holdTime * dragChangeRate, minDrag, maxDrag);
            }
        }

        StopCoroutine(StartAdjustingDrag());
    }

    private IEnumerator StartAdjustingDrag()
    {
        while (isHolding)
        {
            holdTime = Mathf.Clamp(holdTime + Time.deltaTime, 0f, 1f);
            if (DragBar != null)
            {
                DragBar.fillAmount = holdTime;
            }

            yield return null;
        }
    }
}
