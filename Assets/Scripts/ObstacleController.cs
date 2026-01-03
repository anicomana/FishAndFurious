using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    ObstacleSpawnManager obstacleSpawnManager;
    GameObject obstacleSpawnManagerObject;

    void Awake ()
    {
    }
    void Start()
    {
        obstacleSpawnManagerObject = transform.parent.gameObject;

        if (obstacleSpawnManagerObject != null) {
            obstacleSpawnManager = obstacleSpawnManagerObject.GetComponent<ObstacleSpawnManager>();
        }
    }

    void Update()
    {
        transform.Translate(Vector3.right * obstacleSpawnManager.dir * obstacleSpawnManager.obstacleSpeed * Time.deltaTime);

        if (transform.position.x < -obstacleSpawnManager.outBoundSide || transform.position.x > obstacleSpawnManager.outBoundSide) {
            Destroy(gameObject);
        }
        
    }
}
