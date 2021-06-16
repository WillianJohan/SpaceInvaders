using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BottomBarrier : MonoBehaviour
{
    [SerializeField] LayerMask DetectionLayer;

    public static event Action AlienReachedTheBottomMap;

    private void OnTriggerEnter(Collider other)
    {
        if ((DetectionLayer & 1 << other.gameObject.layer) == (1 << other.gameObject.layer))
        {
            AlienReachedTheBottomMap?.Invoke();
            Destroy(gameObject);
        }
    }
}
