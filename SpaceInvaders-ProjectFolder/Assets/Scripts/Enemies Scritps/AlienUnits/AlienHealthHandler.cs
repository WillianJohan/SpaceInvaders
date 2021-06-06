using UnityEngine;

[RequireComponent(typeof(Health))]
public class AlienHealthHandler : MonoBehaviour
{
    [SerializeField] Health health;

    void Awake() => health.OnDie += HandleDie;
    void OnDestroy() => health.OnDie -= HandleDie;

    void HandleDie()
    {
        Destroy(gameObject);
    }

}
