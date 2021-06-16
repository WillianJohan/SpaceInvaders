using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{

    [SerializeField] GameObject endGameScreen;
    [SerializeField] GameObject anyButtonExit;

    [SerializeField] Text HighScoreText;
    [SerializeField] Text CurrentText;

    void Start()        => endGameScreen.SetActive(false);
    void Awake()
    { 
        PlayerHealthHandler.OnPlayerDie += HandleOnPlayerDie;
        BottomBarrier.AlienReachedTheBottomMap += HandleAlienReachedTheBottomMap;
    }
    void OnDestroy() 
    {
        PlayerHealthHandler.OnPlayerDie -= HandleOnPlayerDie;
        BottomBarrier.AlienReachedTheBottomMap -= HandleAlienReachedTheBottomMap;
    }

    void HandleAlienReachedTheBottomMap()
    {
        HandleScore();

        //Kill Player
        GameObject player = FindObjectOfType<GameManager>().playerInstance;
        player.SetActive(false);

        //Habilita tela de end Game
        StartCoroutine(ActivateEndGameScreen());
    }

    void HandleOnPlayerDie()
    {
        HandleScore();

        //Habilita tela de end Game
        StartCoroutine(ActivateEndGameScreen());
    }

    void HandleScore()
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
