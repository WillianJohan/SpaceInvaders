using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Prefabs")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject barrierPrefab;

    [Header("Spawn Reference")]
    [SerializeField] Transform playerSpawnLocation;
    [SerializeField] Transform barrierSpawnLocation;
    [SerializeField] AlienSpawner alienSpawner;

    [Header("Spawn Atributte")]
    [SerializeField] float spawnVelocity = 0.2f;

    GameObject playerInstance;
    GameObject barrierInstance;

    bool isSpawningAliens = false;


    public int AliensAlive { get; private set; } = 0;


    public static event Action StartGame;
    public static event Action StartingNewWave;

    #region Standard Unity Methods

    protected virtual void Awake()
    {
        base.Awake();

        AlienSpawner.OnStartSpawningAliens += HandleOnStartSpawningAliens;
        AlienSpawner.OnFinishedSpawningAliens += HandleOnFinishedSpawningAliens;

        AlienHealthHandler.OnAlienSpawn += HandleAlienSpawn;
        AlienHealthHandler.OnAlienDie += HandleAlienDie;
    }

    void OnDestroy()
    {
        AlienSpawner.OnStartSpawningAliens -= HandleOnStartSpawningAliens;
        AlienSpawner.OnFinishedSpawningAliens -= HandleOnFinishedSpawningAliens;

        AlienHealthHandler.OnAlienSpawn -= HandleAlienSpawn;
        AlienHealthHandler.OnAlienDie -= HandleAlienDie;
    }

    private void Start() => StartCoroutine(StartNewGame());

    #endregion

    #region IEnumerators Methods

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
        alienSpawner.SpawnAliens();

        while (isSpawningAliens)
            yield return new WaitForSeconds(0.1f);

        StartGame?.Invoke();

        yield return null;
    }

    IEnumerator NewWave()
    {
        StartingNewWave?.Invoke();

        //Destroy Barrier
        Destroy(barrierInstance);
        yield return new WaitForSeconds(spawnVelocity);
        

        //instantiate a new Barrier
        barrierInstance = Instantiate(barrierPrefab, barrierSpawnLocation.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnVelocity);

        //Enemies Spawn
        alienSpawner.SpawnAliens();

        while (isSpawningAliens)
            yield return new WaitForSeconds(0.1f);

        StartGame?.Invoke();
        yield return null;
    }

    #endregion

    #region Handle event Methods

    void HandleOnStartSpawningAliens()      => isSpawningAliens = true;
    void HandleOnFinishedSpawningAliens()   => isSpawningAliens = false;

    void HandleAlienSpawn(AlienType type)   => AliensAlive++;
    void HandleAlienDie(AlienType type)
    {
        AliensAlive--;
        if (AliensAlive == 0)
            StartCoroutine(NewWave());
    }

    #endregion

}
