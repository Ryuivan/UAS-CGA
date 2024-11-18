using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed = false;
    public List<GameObject> activeFruits = new List<GameObject>();
    public float Force;

    void Update()
    {
        if (isPressed)
        {
            for (int i = activeFruits.Count - 1; i >= 0; i--)
            {
                GameObject obj = activeFruits[i];
                if (obj != null && !obj.GetComponent<FruitsController>().IsShot())
                {
                    obj.transform.Translate(Force * Time.deltaTime, 0, 0);
                }
                else
                {
                    activeFruits.RemoveAt(i);
                }
            }
        }
    }

    public void AddFruit(GameObject fruit)
    {
        if (fruit != null)
        {
            activeFruits.Add(fruit);
            Debug.Log("Fruit added to right move: " + fruit.name);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
