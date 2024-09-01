using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    private float timeAlive = 0f;
    public List<GameObject> objectsToSpawn; 
    public List<Transform> spawnPoints; 
    public float minSpawnDelay = 1f; 
    public float maxSpawnDelay = 5f; 
    public Transform parent;
    public int moreSpawnsTime = 30;

    void Awake()
    {
        StartCoroutine(SpawnEnemies());
        minSpawnDelay = 1f;
        maxSpawnDelay = 5f;
        timeAlive = 0f;
    }
    void Update(){
        if(timeAlive > 30){
            maxSpawnDelay = 3;
        }
        if(timeAlive > 60){
            minSpawnDelay = 0.5f;
            maxSpawnDelay = 2.5f;
        }
        timeAlive += Time.deltaTime;
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
        newEnemy.Initialize(spawnPoint.right * -1, timeAlive);
    }
}
