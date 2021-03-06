using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienCombatBehaviour : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] LayerMask BarrierLayer;
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] LayerMask AlienLayer;

    [Header("Shoot variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileInitialPosition;
    float checkPlayerPosition_DelayTime;
    float lastCheck = 0;

    public static bool IsEnabled = false;

    public static event Action OnShoot;

    #region Unity Standard methods

    void Awake()
    {
        GameManager.StartGame += HandleStartGame;
        GameManager.StartingNewWave += HandleStartingNewWave;
    }

    void OnDestroy()
    {
        GameManager.StartGame -= HandleStartGame;
        GameManager.StartingNewWave -= HandleStartingNewWave;
    }

    void Start()
    {
        checkPlayerPosition_DelayTime = UnityEngine.Random.Range(1f, 3);
        IsEnabled = false;
    }
    void Update()       => ExecuteCombatBehaviour();

    #endregion

    void ExecuteCombatBehaviour()
    {
        if (!IsEnabled)
            return;

        lastCheck += Time.deltaTime;

        if (lastCheck <= checkPlayerPosition_DelayTime)
            return;

        lastCheck = 0;

        Ray leftRay = new Ray(projectileInitialPosition.position - Vector3.right, -Vector3.up);
        Ray middleRay = new Ray(projectileInitialPosition.position - Vector3.right, -Vector3.up);
        Ray rightRay = new Ray(projectileInitialPosition.position - Vector3.right, -Vector3.up);

        //Checks if have a alien bellow
        if (Physics.Raycast(leftRay, 10, AlienLayer) ||
            Physics.Raycast(middleRay, 10, AlienLayer) ||
            Physics.Raycast(rightRay, 10, AlienLayer))
            return;

        if (!Physics.Raycast(projectileInitialPosition.position, -Vector3.up, out RaycastHit hitInfo, 100, PlayerLayer))
            return;

        if (((AlienLayer & 1 << hitInfo.transform.gameObject.layer) != (1 << hitInfo.transform.gameObject.layer)))
            Instantiate(projectilePrefab, projectileInitialPosition.position, Quaternion.identity);

        OnShoot?.Invoke();
    }

    void TryKillPlayer(Collider other)
    {
        if (!other.TryGetComponent<IDamageable>(out IDamageable otherDamageable))
            return;

        otherDamageable.InstantKill();
    }

    void OnTriggerEnter(Collider other)
    {
        if ((BarrierLayer & 1 << other.gameObject.layer) == (1 << other.gameObject.layer))
            Destroy(other.gameObject);

        if ((PlayerLayer & 1 << other.gameObject.layer) == (1 << other.gameObject.layer))
            TryKillPlayer(other);
    }

    private void HandleStartGame() => IsEnabled = true;
    private void HandleStartingNewWave() => IsEnabled = false;
    

}
