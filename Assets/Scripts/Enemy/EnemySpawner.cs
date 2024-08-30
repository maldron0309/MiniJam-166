using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn; 
    public List<Transform> spawnPoints; 
    public float minSpawnDelay = 1f; 
    public float maxSpawnDelay = 5f; 

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnRandomObject();
            
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnRandomObject()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[randomSpawnIndex];

        int randomObjectIndex = Random.Range(0, objectsToSpawn.Count);
        GameObject objectToSpawn = objectsToSpawn[randomObjectIndex];

        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
