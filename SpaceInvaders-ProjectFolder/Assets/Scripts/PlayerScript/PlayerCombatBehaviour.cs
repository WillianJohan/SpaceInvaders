using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatBehaviour : MonoBehaviour
{
    public KeyCode key_Shoot1 = KeyCode.Space;
    public KeyCode key_Shoot2 = KeyCode.UpArrow;

    [Header("Combat Variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileInitialPosition;
    [SerializeField] float shootDelay = 0.1f;
    [SerializeField] int maximumActiveProjectilesSimultaneously = 3;
    List<GameObject> bulletInstances = new List<GameObject>();

    bool CanShot = false;

    //public static event Action<GameObject> OnShoot;
    public static event Action OnShoot;

    #region Unity Standard Methods

    void Awake()
    {
        GameManager.StartGame += HandleInitGame;
        GameManager.StartingNewWave += HandleStartingNewWave;
    }

    void OnDestroy()
    {
        GameManager.StartGame -= HandleInitGame;
        GameManager.StartingNewWave -= HandleStartingNewWave;
    }

    void Update()
    {
        CheckProjectileInstances();
        HandleShotCommand();
    }
    
    #endregion

    #region Combat Behaviour Methods

    void CheckProjectileInstances()
    {
        //Removes the projectiles that were destroyed
        for (int i = 0; i < bulletInstances.Count; i++)
            if (bulletInstances[i] == null)
                bulletInstances.RemoveAt(i);
    }

    void HandleShotCommand()
    {
        if (!CanShot)
            return;
        
        bool hasAmmo = maximumActiveProjectilesSimultaneously > bulletInstances.Count;
        bool wantToShoot = (Input.GetKey(key_Shoot1) || Input.GetKey(key_Shoot2) || Input.GetMouseButton(0));
        
        if (wantToShoot && hasAmmo)
            StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        CanShot = false;
        bulletInstances.Add(Instantiate(projectilePrefab, projectileInitialPosition.position, Quaternion.identity));
        OnShoot?.Invoke();
        yield return new WaitForSeconds(shootDelay);
        CanShot = true;
    }

    #endregion

    #region Handle event Methods

    private void HandleInitGame()
    {
        CanShot = true;
    }

    private void HandleStartingNewWave()
    {
        CanShot = false;
    }

    #endregion

}
