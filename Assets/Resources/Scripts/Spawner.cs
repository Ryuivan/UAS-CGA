using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3 SpawnPos;
    public GameObject[] spawnObjects;
    private float newSpawnDuration = 1.0f;
    private int currentIndex = 0;

    #region Singleton

    public static Spawner instance;

    private void Awake() 
    {
        instance = this;
    }

    #endregion

    private void Start() 
    {
        SpawnPos = transform.position;
        SpawnRandomFruit();
    }
    
    void SpawnRandomFruit() 
    {
        int randomIndex = Random.Range(0, spawnObjects.Length);
        GameObject newFruit = Instantiate(spawnObjects[randomIndex], SpawnPos, Quaternion.identity);

        LeftMove leftMove = FindObjectOfType<LeftMove>();
        if (leftMove != null)
        {
            leftMove.AddFruit(newFruit);
        }

        RightMove rightMove = FindObjectOfType<RightMove>();
        if (rightMove != null)
        {
            rightMove.AddFruit(newFruit);
        }
    }

    void SpawnNewObject() 
    {
        GameObject newFruit = Instantiate(spawnObjects[currentIndex], SpawnPos, Quaternion.identity);
        currentIndex = (currentIndex + 1) % spawnObjects.Length;

        LeftMove leftMove = FindObjectOfType<LeftMove>();
        if (leftMove != null)
        {
            leftMove.AddFruit(newFruit);
        }

        RightMove rightMove = FindObjectOfType<RightMove>();
        if (rightMove != null)
        {
            rightMove.AddFruit(newFruit);
        }
    }

    public void StartSpawning() 
    {
        Invoke("SpawnNewObject", newSpawnDuration);
    }
}
