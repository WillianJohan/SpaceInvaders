using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject barrierPrefab;
    [SerializeField] GameObject enemiesPrefab;

    [Header("Spawn Reference")]
    [SerializeField] Transform playerSpawnLocation;
    [SerializeField] Transform barrierSpawnLocation;
    [SerializeField] Transform enemiesSpawnLocation;

    [Header("etc")]
    [SerializeField] float spawnVelocity = 0.2f;

    private void Start()
    {
        StartCoroutine(StartNewGame());
    }

    IEnumerator StartNewGame()
    {
        //Wait a time after spawn the game objects
        yield return new WaitForSeconds(0.5f);

        //Player Spawn
        Instantiate(playerPrefab, playerSpawnLocation.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnVelocity);
        
        //Barrier Spawn
        Instantiate(barrierPrefab, barrierSpawnLocation.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnVelocity);
        
        //Enemies Spawn
        Instantiate(enemiesPrefab, enemiesSpawnLocation.position, Quaternion.identity);
        
        
        yield return null;
    }

}
