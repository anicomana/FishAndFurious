using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event System.Action OnNewMaxReached;
    public event System.Action MovedBackTooMuch;
    public event System.Action OnNewBonusReached;
    public event System.Action OnNewBonusPoints;
    public float bonusPoints;
    public float maxSectionReached;


    //where to listen events from
    private GroundManager groundManager;
    private GameObject groundManagerObject;
    private GameManager gameManager;
    private GameObject gameManagerObject;
    private GameObject startingBase;
    private PlayerController playerController;
    private GameObject playerControllerObject;

    private float currentSection;
    private int minSectionsUntilBonus = 5;
    private int maxSectionsUntilBonus = 10;
    private int sectionsUntilNextBonus;
    void Awake()
    {
        groundManagerObject = GameObject.Find("_GroundManager");
        gameManagerObject = GameObject.Find("_GameManager");
        startingBase = GameObject.FindGameObjectWithTag("StartingBase");
        playerControllerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        ResetSCore();
        ResetSectionsUntilBonus();

        if (gameManagerObject !=null) {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            gameManager.OnGameOver += CalculateFinalScore;
        }

        if(groundManagerObject !=null) {
            groundManager = groundManagerObject.GetComponent<GroundManager>();
            groundManager.OnMovedForward += AddCurrentSection;
            groundManager.OnMovedBackward += RemoveCurrentSection;
        }

        if (playerControllerObject != null) {
            playerController = playerControllerObject.GetComponent<PlayerController>();
            playerController.OnBonusCollision += AddBonusPoints;
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
        currentSection++;

        if (currentSection > maxSectionReached) {
            maxSectionReached = currentSection;
            OnNewMaxReached?.Invoke();

            sectionsUntilNextBonus--;

            //Invoke event to let listeners know that is time to spawn new bonus
            if(sectionsUntilNextBonus <= 0) {
                ResetSectionsUntilBonus();
                OnNewBonusReached?.Invoke();
            }
        }
    }

    //remove point when going backwards
    void RemoveCurrentSection()
    {
        currentSection--;
    }
    
    //to be called when Restart event is Invoked
    void ResetSCore()
    {
        currentSection = -startingBase.transform.position.z;
        maxSectionReached = currentSection;
    }
    //Calculate on which section next bonus should spawn
    void ResetSectionsUntilBonus()
    {
        sectionsUntilNextBonus = Random.Range(minSectionsUntilBonus, maxSectionsUntilBonus + 1);
    }

    //Calculate final score = Roads crossed + bonus value
    void CalculateFinalScore()
    {
        float finalScore = maxSectionReached + bonusPoints;
    }

    void AddBonusPoints(int point)
    {
         bonusPoints += point;
         OnNewBonusPoints?.Invoke();
    }
}  
