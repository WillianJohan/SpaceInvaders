using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ProjectileBehaviour : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] LayerMask DamageableLayer;
    [SerializeField] LayerMask BarrierLayer;
    
    [Header("Projectile behaviour values")]
    [SerializeField] int projectileDamage = 1;
    [SerializeField] float projectileVelocity = 20.0f;
    [SerializeField] float destroyAfter = 5.0f;
    [SerializeField] float destructionRadius = 0.5f;
    
    [SerializeField] ProjectileDirection projectileDirection = ProjectileDirection.up;
    

    enum ProjectileDirection { up = 1, down = -1}
    Rigidbody rigidbody;


    void Start()
    {
        Vector3 direction = Vector3.up * (int)projectileDirection * projectileVelocity;
        GetComponent<Rigidbody>().velocity = direction;
        Destroy(gameObject, destroyAfter);
    }


    void OnTriggerEnter(Collider other)
    {
        //verifica se o alvo atingido é uma barreira ou o alvo marcado como target
        if ((DamageableLayer & 1 << other.gameObject.layer) == (1 << other.gameObject.layer))
            TryDealDamage(other);
        else if ((BarrierLayer & 1 << other.gameObject.layer) == (1 << other.gameObject.layer))
            TryDestroyBarrier(other);

        Destroy(gameObject);
    }

    void TryDealDamage(Collider other)
    {
        if (!other.TryGetComponent<IDamageable>(out IDamageable otherDamageable))
            return;

        otherDamageable.DealDamage(projectileDamage);
    }

    void TryDestroyBarrier(Collider other)
    {
        Destroy(other.gameObject);
        Collider[] colliders = Physics.OverlapSphere(transform.position, destructionRadius, BarrierLayer);
        foreach (Collider coll in colliders)
            Destroy(coll.gameObject);

    }
}
