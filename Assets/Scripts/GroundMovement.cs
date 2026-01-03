using UnityEngine;

//created a new type of variable: Direction that has, in this case, two values
enum Direction { Forward, Backward }

public class GroundMovement : MonoBehaviour
{
    //to read variables from
    PlayerController playerController;
    GameObject player;

    //where to listen events from
    GroundManager groundManager;
    GameObject groundManagerObject;

    GameManager gameManager;
    GameObject gameManagerObject;
    private float startingBaseAsideYAxis = -5f;
    private Vector3 groundTargetPos;
    private Vector3 startingBaseInitialPos;
    private bool isGroundMoving;
    private bool isStartingBaseAside;
    

    //on awake finds gameobjects in scene
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        groundManagerObject = GameObject.Find("_GroundManager");
        gameManagerObject = GameObject.Find("_GameManager");
    }

    void Start()
    {
        groundTargetPos = transform.position;

        //check that player and groundManager are not null and the fetchin the components
        if (player != null) {
            playerController = player.GetComponent<PlayerController>();
        }

        if (groundManagerObject != null) {
            groundManager = groundManagerObject.GetComponent<GroundManager>();
            groundManager.OnMovedForward += MoveForward; //subscribes to event by groundManager
            groundManager.OnMovedBackward += MoveBackward; //subscribes to event by groundManager
        }

        if (gameManagerObject != null) {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            gameManager.OnGameReset += ResetGroundPosition;
        }

        if(gameObject.CompareTag("StartingBase")) {
            startingBaseInitialPos = transform.position;
        }

    }

    void Update()
    {
        //moves the section towards the target that has move
        if (isGroundMoving) {
            transform.position = Vector3.MoveTowards(transform.position, groundTargetPos, playerController.moveSpeed * Time.deltaTime);

            //if close enough, snaps to target's position
            //when section snaps to its target, calls for SpawnNewSectionIfNeeded that checks if a new section should spawn
            if (Vector3.Distance(transform.position, groundTargetPos) < playerController.posThreshold) {
                transform.position = groundTargetPos;
                isGroundMoving = false;
                groundManager.SpawnNewSectionIfNeeded(); //after the movement is made calls method to spawn if needed
            }
            return;
        }

        if (transform.position.z <= groundManager.outBoundBottom){
            if(gameObject.CompareTag("StartingBase")) {
                transform.position = new Vector3(transform.position.x, startingBaseAsideYAxis, groundManager.outBoundBottom);
                isStartingBaseAside = true;
            } else {
                OnCleanUpAndDestroy();
            }
        }
    }

    //when event is announced, calls Move method with input "type" Direction "value" Forward or Backward
    void MoveForward()
    {
        Move(Direction.Forward);

    }

    void MoveBackward()
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
        if (isStartingBaseAside == false) {
            groundTargetPos += dir * playerController.stepSizeXAxis;
            isGroundMoving = true;
        }
    }

    void ResetGroundPosition()
    {
        if (gameObject.CompareTag("StartingBase")) {
            transform.position = startingBaseInitialPos;
        } else {
            OnCleanUpAndDestroy();
        }
        
        groundTargetPos = transform.position;
        isStartingBaseAside = false;
    }
    
    void OnCleanUpAndDestroy()
    {
            if (groundManager != null) {
            groundManager.OnMovedForward -= MoveForward;
            groundManager.OnMovedBackward -= MoveBackward;
        }

        if (gameManager != null) {
            gameManager.OnGameReset -= ResetGroundPosition;
        }

        Destroy(gameObject);

    }
}

