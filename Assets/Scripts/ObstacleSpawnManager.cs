using UnityEngine;
using System.Collections;

 
public class ObstacleSpawnManager : MonoBehaviour
{
    PlayerController playerController;
    GameObject player;

    public int dir;
    public GameObject[] obstaclesToSpawn;

    public float spawnDelay = 2f;
    public float obstacleSpawnPosX = 7f;
    public float minSpawnDelay = 2f;
    public float maxSpawnDelay = 5f;
    public float outBoundSide = 12f;
    public float minObstacleSpeed = 2f;
    public float maxObstacleSpeed = 4f;

    public float obstacleSpeed;
    
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    void Start()
    {
        if (player != null) {
            playerController = player.GetComponent<PlayerController>();
        }

        float randomValue;
        randomValue = Random.value;

        if (randomValue < 0.5f) {
            dir = -1;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        } else {
            dir = 1;
        }

        obstacleSpeed = Random.Range(minObstacleSpeed, maxObstacleSpeed);
        obstacleSpawnPosX *= -dir;
        InstantiateRandomObstacle();
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
