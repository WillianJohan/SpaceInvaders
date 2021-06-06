using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienCombatBehaviour : MonoBehaviour
{
    [SerializeField] LayerMask BarrierLayer;
    [SerializeField] LayerMask PlayerLayer;

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
