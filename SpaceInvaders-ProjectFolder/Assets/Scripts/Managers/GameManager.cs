using System;
using System.Collections;
using System.Collections.Generic;
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

    public GameObject playerInstance { get; private set; }
    public GameObject barrierInstance { get; private set; }

    bool isSpawningAliens = false;


    public int AliensAlive { get; private set; } = 0;


    public static event Action StartGame;
    public static event Action StartingNewWave;
    public static event Action OnSpawnElement;

    #region Standard Unity Methods

    protected override void Awake()
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
        OnSpawnElement?.Invoke();
        playerInstance = Instantiate(playerPrefab, playerSpawnLocation.position, Quaternion.identity);
        
        yield return new WaitForSeconds(spawnVelocity);

        //Barrier Spawn
        OnSpawnElement?.Invoke();
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

        //Destroy Projectiles
        List<ProjectileBehaviour> projectiles = new List<ProjectileBehaviour>();
        projectiles.AddRange(FindObjectsOfType<ProjectileBehaviour>());
        for(int i = projectiles.Count; i > 0; i--)
        {
            Destroy(projectiles[i - 1].gameObject);
            projectiles.RemoveAt(i - 1);
        }


        //instantiate a new Barrier
        barrierInstance = Instantiate(barrierPrefab, barrierSpawnLocation.position, Quaternion.identity);
        OnSpawnElement?.Invoke();
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
