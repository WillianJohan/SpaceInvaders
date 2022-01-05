using System;
using UnityEngine;

public class AlienBossBehaviour : Singleton<AlienBossBehaviour>
{
    [SerializeField] float velocity = 10f;
    [SerializeField] LayerMask invisibleBarrierLayer;

    public static event Action OnBossDie;

    void OnDestroy()
    {
        //Remove a referencia da instancia quando o obj for destruido.
        Instance = null;
        OnBossDie?.Invoke();
    }

    void Update()
        => transform.Translate(Vector3.right * velocity * Time.deltaTime);

    void OnTriggerEnter(Collider other)
    {
        if ((invisibleBarrierLayer & 1 << other.gameObject.layer) != (1 << other.gameObject.layer))
            return;

        Destroy(this.gameObject);
    }
}
