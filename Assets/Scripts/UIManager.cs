using UnityEngine;
using System.Collections;


public class UIManager : MonoBehaviour
{
    
    public GameObject inGameScreen;
    public GameObject gameOverScreen;
    
    private GameManager gameManager;
    private GameObject gameManagerObject;

    void Awake ()
    {
        gameManagerObject = GameObject.Find("_GameManager");
    }

    void Start()
    {
        if (gameManagerObject != null) {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            gameManager.OnGameOver += () => {
                gameOverScreen.SetActive(true);
                inGameScreen.SetActive(false); };
        }
    }

    void Update()
    {
        
    }
}

