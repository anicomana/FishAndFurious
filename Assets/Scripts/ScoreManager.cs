using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event System.Action OnNewMaxReached;
    public event System.Action MovedBackTooMuch;

    //where to listen events from
    GroundManager groundManager;
    GameObject groundManagerObject;
    GameManager gameManager;
    GameObject gameManagerObject;
    GameObject startingBase;

    private float currentSection;
    private float maxSectionReached;
    private int scoreGainedPerSection = 1;
    void Awake()
    {
        groundManagerObject = GameObject.Find("_GroundManager");
        gameManagerObject = GameObject.Find("_GameManager");
        startingBase = GameObject.FindGameObjectWithTag("StartingBase");
    }

    void Start()
    {
        ResetSCore();

        if (gameManagerObject !=null) {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            gameManager.OnGameOver += CalculateFinalScore;
            gameManager.OnGameReset += ResetSCore;
        }

        if(groundManagerObject !=null) {
            groundManager = groundManagerObject.GetComponent<GroundManager>();
            groundManager.OnMovedForward += AddCurrentSection;
            groundManager.OnMovedBackward += RemoveCurrentSection;
        }
    }

    void Update()
    {
        float diff = maxSectionReached - currentSection;
        if (diff >= gameManager.maxBackwardsSteps) {
            MovedBackTooMuch?.Invoke();
        }
    }

    //add points when going forward
    void AddCurrentSection()
    {
        currentSection += scoreGainedPerSection;

        if (currentSection > maxSectionReached) {
            maxSectionReached = currentSection;
            OnNewMaxReached.Invoke();
            Debug.Log("Current score is: " + maxSectionReached);
        }
    }

    //remove point when going backwards
    void RemoveCurrentSection()
    {
        currentSection -= scoreGainedPerSection;
    }

    void CalculateFinalScore()
    {
        Debug.Log("Your final score is: " + maxSectionReached);
    }
    //to be called when Restart event is Invoked
    void ResetSCore()
    {
        currentSection = -startingBase.transform.position.z;
        maxSectionReached = currentSection;
    }
}  
