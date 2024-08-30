using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn; 
    public List<Transform> spawnPoints; 
    public float minSpawnDelay = 1f; 
    public float maxSpawnDelay = 5f; 
    public Transform parent;
    public int moreSpawnsTime = 20;

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
        int randomSpawnIndex;
        if(Time.time < moreSpawnsTime){
            randomSpawnIndex = Random.Range(0, spawnPoints.Count-2);
        }
        else{
            randomSpawnIndex = Random.Range(0, spawnPoints.Count);
        }
        Transform spawnPoint = spawnPoints[randomSpawnIndex];

        int randomObjectIndex = Random.Range(0, objectsToSpawn.Count);
        GameObject objectToSpawn = objectsToSpawn[randomObjectIndex];

        GameObject newEnemyObject = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        newEnemyObject.transform.parent = parent;
        Enemy newEnemy = newEnemyObject.GetComponent<Enemy>();
        newEnemy.Initialize(spawnPoint.right * -1);
    }
}
