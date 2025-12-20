using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    PlayerController playerController;
    GameObject player;

    public event System.Action OnMoved;

    private Vector3 groundTargetPos;
    private bool groundMoving;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        groundTargetPos = transform.position;

        if (player != null) {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (groundMoving) {
            transform.position = Vector3.MoveTowards(transform.position, groundTargetPos, playerController.moveSpeed * Time.deltaTime);
            OnMoved?.Invoke();

            if (Vector3.Distance(transform.position, groundTargetPos) < playerController.posThreshold) {
                transform.position = groundTargetPos;
                groundMoving = false;
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
            groundTargetPos += dir * playerController.stepSizeXAxis;
            groundMoving = true;
        }
    }
}
