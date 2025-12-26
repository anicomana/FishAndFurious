using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    ObstacleSpawnManager obstacleSpawnManager;
    GameObject obstacleSpawnManagerObject;

    public float obstacleSpeed = 3f;

    void Awake ()
    {
    }
    void Start()
    {
        obstacleSpawnManagerObject = transform.parent.gameObject;

        if (obstacleSpawnManagerObject != null) {
            obstacleSpawnManager = obstacleSpawnManagerObject.GetComponent<ObstacleSpawnManager>();
        } else {
            Debug.Log("Component not found");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.right * obstacleSpawnManager.dir * obstacleSpeed * Time.deltaTime);
    }
}
