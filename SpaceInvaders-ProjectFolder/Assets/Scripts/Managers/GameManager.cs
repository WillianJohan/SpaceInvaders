using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Prefabs")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject barrierPrefab;
    [SerializeField] GameObject enemiesPrefab;

    [Header("Spawn Reference")]
    [SerializeField] Transform playerSpawnLocation;
    [SerializeField] Transform barrierSpawnLocation;
    [SerializeField] Transform enemiesSpawnLocation;

    [Header("Spawn Atributte")]
    [SerializeField] float spawnVelocity = 0.2f;

    GameObject playerInstance;
    GameObject barrierInstance;
    GameObject aliensInstance;

    public static event Action StartGame;
    public static event Action StartPreparingNewWave;
    public static event Action NewWaveBegin;

    private void Start() => StartCoroutine(StartNewGame());

    IEnumerator StartNewGame()
    {
        //Wait a time after spawn the game objects
        yield return new WaitForSeconds(0.5f);

        //Player Spawn
        playerInstance = Instantiate(playerPrefab, playerSpawnLocation.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnVelocity);
        
        //Barrier Spawn
        barrierInstance = Instantiate(barrierPrefab, barrierSpawnLocation.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnVelocity);
        
        //Enemies Spawn
        aliensInstance = Instantiate(enemiesPrefab, enemiesSpawnLocation.position, Quaternion.identity);

        StartGame?.Invoke();

        yield return null;
    }

    IEnumerator NewWave()
    {
        StartPreparingNewWave?.Invoke();

        Destroy(aliensInstance);
        Destroy(barrierInstance);

        yield return new WaitForSeconds(spawnVelocity);

        barrierInstance = Instantiate(barrierPrefab, barrierSpawnLocation.position, Quaternion.identity);
        
        yield return new WaitForSeconds(spawnVelocity);

        aliensInstance = Instantiate(enemiesPrefab, enemiesSpawnLocation.position, Quaternion.identity);

        NewWaveBegin?.Invoke();
    }
}