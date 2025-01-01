using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3 SpawnPos;
    public GameObject[] spawnObjects;
    private float newSpawnDuration = 1.0f;
    private int currentIndex = 0;
    private float despawnTime = 5f;

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
        StartCoroutine(CheckAndSpawn());
    }

    void SpawnRandomFruit()
    {
        int randomIndex = Random.Range(0, spawnObjects.Length);
        GameObject newFruit = Instantiate(spawnObjects[randomIndex], SpawnPos, Quaternion.identity);

        Fruit fruitComponent = newFruit.AddComponent<Fruit>();
        fruitComponent.SetDespawnTime(despawnTime);

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

        Fruit fruitComponent = newFruit.AddComponent<Fruit>();
        fruitComponent.SetDespawnTime(despawnTime);

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

    private IEnumerator CheckAndSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            
            if (GameObject.FindObjectsOfType<Fruit>().Length == 0)
            {
                SpawnRandomFruit();
            }
        }
    }
}
