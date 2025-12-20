using UnityEngine;

public class WorldMove : MonoBehaviour
{
    PlayerController playerController;
    GameObject player;

    private Vector3 worldTargetPos;
    private bool worldMoving;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        worldTargetPos = transform.position;

        if (player != null) {
            playerController = player.GetComponent<PlayerController>();
            Debug.Log("Script found!");
            }
    }

    void Update()
    {
        if (worldMoving) {
            transform.position = Vector3.MoveTowards(transform.position, worldTargetPos, playerController.moveSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, worldTargetPos) < playerController.posThreshold) {
                transform.position = worldTargetPos;
                worldMoving = false;
            }
            return;
        }


        Vector3 dir = Vector3.zero;

         if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            dir = Vector3.back;
        } else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            dir = Vector3.forward;
        }
        
        if (dir != Vector3.zero) {
            worldTargetPos += dir * playerController.stepSizeXAxis;
            worldMoving = true;        }
    }
}
