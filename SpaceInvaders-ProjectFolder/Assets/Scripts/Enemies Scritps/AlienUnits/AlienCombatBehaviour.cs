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

    private void Start()
    {
        checkPlayerPosition_DelayTime = Random.Range(3, 8);
    }

    void Update()
    {
        //Check if the player is below 
        //Raycast down

        lastCheck += Time.deltaTime;

        if (lastCheck < checkPlayerPosition_DelayTime)
            return;

        if(!Physics.Raycast(projectileInitialPosition.position, -Vector3.up, out RaycastHit hitInfo))
            return;

        if (((AlienLayer & 1 << hitInfo.transform.gameObject.layer) == (1 << hitInfo.transform.gameObject.layer)))
            return;

        Instantiate(projectilePrefab, projectileInitialPosition.position, Quaternion.identity);

        lastCheck = 0;

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
}
