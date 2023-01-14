using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public static event Action OnImpact;

    [SerializeField] LayerMask _collisionMask;
    [SerializeField] private float _speed;

    [SerializeField] private float _lifetime = 2.0f;

    [SerializeField] private float _damage;
    [SerializeField] private float _maxRange;


    [SerializeField] float _dotDamage;
    [SerializeField] float _dotDuration;



    private enum SpellType { PROJECTILE, AOE }
    [SerializeField] private SpellType _spellType;

    [SerializeField] GameObject _dotVFX;

    [SerializeField] GameObject _impactVFX;

    [SerializeField] private float _impactRadius;
    [SerializeField] private float _impactForce;


    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }
    void Update()
    {
        
        float moveDistance = _speed * Time.deltaTime;
        if (_spellType == SpellType.PROJECTILE)
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        CheckCollisions();

    }

    private void CheckCollisions()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _collisionMask))
        {
            HandleImpact(hit);
        }
    }

    private void HandleImpact(RaycastHit hit)
    {
        OnImpact?.Invoke();
        if (_impactVFX) SpawnImpactVFX(hit);

        // See if the target is Damageable, if it is - deal initial damage
        IDamagable damagable = hit.transform.GetComponent<IDamagable>();
        damagable?.TakeDamage(_damage);

        damagable?.DamageOverTime(_dotDamage, _dotDuration, 0.4f);

        Collider[] colliders = Physics.OverlapSphere(hit.point, _impactRadius, _collisionMask, QueryTriggerInteraction.Collide);
        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_impactForce, hit.point, _impactRadius);
            }
        }

        Die();

    }

    private void SpawnImpactVFX(RaycastHit hit)
    {
        Instantiate(_impactVFX, hit.point, Quaternion.Euler(hit.normal));
    }

    private void OnTriggerEnter(Collider other)
    {
        // IDamagable damagable = other.GetComponent<IDamagable>();
        // damagable?.TakeDamage(_damage);

    }

    private void Die()
    {
        if (_impactVFX != null)
        {
            // Instantiate(_impactVFX, )
        }
        Destroy(gameObject);
    }
}
