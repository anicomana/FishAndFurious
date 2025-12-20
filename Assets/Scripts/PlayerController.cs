using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject player;

    public float stepSizeSide = 1f;
    public float stepSizeXAxis = 2f;
    public float moveSpeed = 15f;
    public float posThreshold = 0.001f;
    
    private Vector3 targetPos;
    private bool moving;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, targetPos) < posThreshold) {
                transform.position = targetPos;
                moving = false;
            }
            return;
        }

        Vector3 dir = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            dir = Vector3.left;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            dir = Vector3.right;
        }
        
        if (dir != Vector3.zero) {
            targetPos += dir * stepSizeSide;
            moving = true;
        }

    }
}
