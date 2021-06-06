using System;
using UnityEngine;

public class AlienDetectionLimit : MonoBehaviour
{
    [SerializeField] GameObject OtherBarrier;
    [SerializeField] LayerMask DetectionLayer;

    public static event Action AlienReachedTheLimit;

    void OnTriggerEnter(Collider other)
    {
        if ((DetectionLayer & 1 << other.gameObject.layer) != (1 << other.gameObject.layer))
            return;

        AlienReachedTheLimit?.Invoke();
        OtherBarrier.SetActive(true);
        gameObject.SetActive(false);
    }

}
