using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{

    [SerializeField] GameObject endGameScreen;
    [SerializeField] GameObject anyButtonExit;

    [SerializeField] Text HighScoreText;
    [SerializeField] Text CurrentText;

    public static event Action EndGame;


    void Start()        => endGameScreen.SetActive(false);
    void Awake()
    { 
        PlayerHealthHandler.OnPlayerDie += HandleEndGame;
        BottomBarrier.AlienReachedTheBottomMap += HandleEndGame;
    }
    void OnDestroy() 
    {
        PlayerHealthHandler.OnPlayerDie -= HandleEndGame;
        BottomBarrier.AlienReachedTheBottomMap -= HandleEndGame;
    }

    void HandleEndGame()
    {
        EndGame?.Invoke();

        SaveScore();

        //Kill Player
        GameObject player = FindObjectOfType<GameManager>().playerInstance;
        player.SetActive(false);

        //Habilita tela de end Game
        StartCoroutine(ActivateEndGameScreen());
    }

    void SaveScore()
    {
        HighScoreText.text = DataManager.LoadScore().ToString();
        CurrentText.text = ScoreManager.CurrentScore.ToString();

        //Save player Score
        DataManager.SaveScore(ScoreManager.CurrentScore);
    }

    IEnumerator ActivateEndGameScreen()
    {
        yield return new WaitForSeconds(0.5f);

        endGameScreen.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);

        anyButtonExit.SetActive(true);

        yield return null;
    }

}
