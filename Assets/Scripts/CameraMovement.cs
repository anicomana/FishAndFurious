using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    //which script to get variables from
    GroundManager groundManager;
    GameObject groundManagerObject;
    PlayerController playerController;
    GameObject player;

    private Vector3 camTargetPos;
    private Vector3 camStartingPos;
    private bool camMoving;

    void Awake ()
    {
        //find player
        player = GameObject.FindGameObjectWithTag("Player");
        groundManagerObject = GameObject.Find("_GroundManager");
    }
    void Start()
    {
        if (player != null) {
            playerController = player.GetComponent<PlayerController>();
        }

        if (groundManagerObject != null) {
            groundManager = groundManagerObject.GetComponent<GroundManager>();
            groundManager.OnMovedForward += CamMoveForward;
            groundManager.OnMovedBackward += CamMoveBackward;
        }

        camTargetPos = transform.position;
        camStartingPos = transform.position;
    }

    void Update()
    {
        //if (!moving) then transform.position.z moves towards targetpos
        if (camMoving) {
            transform.position = Vector3.MoveTowards(transform.position, camTargetPos, playerController.moveSpeed * Time.deltaTime);
        
                if (Vector3.Distance(transform.position, camTargetPos) < playerController.posThreshold) {
                    transform.position = camTargetPos;
                    camMoving = false;
                }
            return;
        } 
    }

    void CamMoveForward()
    {
        Move(Direction.Forward);
    }

    void CamMoveBackward()
    {
        Move(Direction.Backward);
    }

    //this method can only be called when has in input a value from "type" Direction, that we are calling direction
    void Move(Direction direction)
    {
        Vector3 dir = Vector3.zero;

        //check what "value", from "type" Direction, direction has
        switch (direction)
        {
            case Direction.Forward:
                dir = Vector3.back;
                break;
            case Direction.Backward:
                dir = Vector3.forward;
                break;
        }

        //once dir has been determined, we can calculate new groundTargetPos
        camTargetPos += dir * playerController.stepSizeXAxis;
        camMoving = true;
    }
}
