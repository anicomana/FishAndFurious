using UnityEngine;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    public GameObject inGameScreen;
    public GameObject gameOverScreen;

    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bonusPointsText;
    public TextMeshProUGUI gameOScoreText;
    public TextMeshProUGUI gameOBonusPointsText;
    public TextMeshProUGUI gameOFinalPointsText; 
    

    private ScoreManager scoreManager;
    private GameObject scoreManagerObject;
    private GameManager gameManager;
    private GameObject gameManagerObject;

    void Awake ()
    {
        gameManagerObject = GameObject.Find("_GameManager");
        scoreManagerObject = GameObject.Find("_ScoreManager");
    }

    void Start()
    {
        if (gameManagerObject != null) {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            gameManager.OnGameOver += () => {
                gameOverScreen.SetActive(true);
                inGameScreen.SetActive(false);
                ShowFinalScore(); };
        }

        if (scoreManagerObject != null) {
            scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
            scoreManager.OnNewMaxReached += ShowInGameRoadsUI;
            scoreManager.OnNewBonusPoints += ShowInGameBonusUI;
        }
    }

    void Update()
    {
        
    }

    
    void ShowFinalScore()
    {
        float bonusPoints = scoreManager.bonusPoints;
        float maxSectionReached = scoreManager.maxSectionReached;
        float finalScore = maxSectionReached + bonusPoints;

        gameOBonusPointsText.text = bonusPoints.ToString();
        gameOScoreText.text = maxSectionReached.ToString();
        gameOFinalPointsText.text = "Final Score: " + finalScore;
    }
    

    
    void ShowInGameRoadsUI()
    {
        float maxSectionReached = scoreManager.maxSectionReached;
        if (maxSectionReached > 0 ) {
            scoreText.text = "Roads crossed: " + maxSectionReached;
        }
            
    }
    

    
    void ShowInGameBonusUI()
    {
        float bonusPoints = scoreManager.bonusPoints;
        bonusPointsText.text = "Bonus Points: " + bonusPoints;
    }
    
}

