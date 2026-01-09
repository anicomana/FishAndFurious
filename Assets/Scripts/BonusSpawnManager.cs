using UnityEngine;

public class BonusSpawnManager : MonoBehaviour
{
    //where to listen for event

    PlayerController playerController;
    GameObject player;
    ScoreManager scoreManager;
    GameObject scoreManagerObject;
    public GameObject bonusToSpawn;

    void Awake()
    {
        //find gameobject
        player = GameObject.FindGameObjectWithTag("Player");
        scoreManagerObject = GameObject.Find("_ScoreManager");
    }

    void Start()
    {
        //if gameobject is not null then get component and subscribe to event to call instantiate bonus
        if (player != null) {
            playerController = player.GetComponent<PlayerController>();
        }

        if (scoreManagerObject != null) {
            scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
            scoreManager.OnNewBonusReached += InstantiateRandomBonus;
        }
    }

    void Update()
    {
        
    }

    public void InstantiateRandomBonus()
    {
        //instantiate bonus Game Object
        int randomPosX = Random.Range(-playerController.playerOutBoundSide, playerController.playerOutBoundSide);
        Vector3 spawnPos = new Vector3(randomPosX, transform.position.y , transform.position.z);
        transform.position = spawnPos;
        Instantiate(bonusToSpawn, transform);
        Debug.Log("instantiate bonus");

    }
}
