using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] Health health;

    void Awake() => health.OnDie += HandleDie;
    void OnDestroy() => health.OnDie -= HandleDie;

    void HandleDie()
    {
        //Chamar o gerenciador o metodo end game ou algo assim
        Destroy(gameObject);
    }

}
