using UnityEngine;
using System.Collections;

 
public class ObstacleSpawnManager : MonoBehaviour
{
    public int dir;
    public GameObject[] obstaclesToSpawn;
    public float spawnDelay = 2f;
    public float obstacleSpawnPosX = 7f;
    public float minSpawnDelay = 2f;
    public float maxSpawnDelay = 5f;


    void Start()
    {
        float randomValue;
        randomValue = Random.value;

        if (randomValue < 0.5f) {
            dir = -1;
        } else {
            dir = 1;
        }

        obstacleSpawnPosX *= -dir;
        StartCoroutine(InstantiateWithDelay());
    }

    void Update()
    {

    }

    IEnumerator InstantiateWithDelay()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(spawnDelay);
        InstantiateRandomObstacle();
        StartCoroutine(InstantiateWithDelay());

    }

    void InstantiateRandomObstacle()
    {
        //to instantiate a random section from the list
        Vector3 obstacleSpawnPos = new Vector3 (obstacleSpawnPosX, transform.position.y , transform.position.z);
        transform.position = obstacleSpawnPos;
        int randomObstacle = Random.Range(0, obstaclesToSpawn.Length);
        Instantiate(obstaclesToSpawn[randomObstacle], transform);
    }
}
