using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{

    [SerializeField] GameObject endGameScreen;

    void Start()        => endGameScreen.SetActive(false);
    void Awake()        => PlayerHealthHandler.OnPlayerDie += HandleOnPlayerDie;
    void OnDestroy()    => PlayerHealthHandler.OnPlayerDie -= HandleOnPlayerDie;

    void HandleOnPlayerDie()
    {
        //Save player Score
        Data.Save(ScoreManager.CurrentScore);

        //Habilita tela de end Game
        StartCoroutine(ActivateEndGameScreen());
    }

    IEnumerator ActivateEndGameScreen()
    {
        yield return new WaitForSeconds(0.5f);

        endGameScreen.SetActive(true);

        yield return null;
    }

}
