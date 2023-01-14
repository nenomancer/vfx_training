using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Ranged : Ability
{

    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _projectileRadius;
    protected float _currentRange;


    private SphereCollider _collider;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        _currentRange = 0.0f;
        // Destroy(gameObject, _baseRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentRange < _baseRange)
        {

            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
            _currentRange += Time.deltaTime * _moveSpeed;
        }
        else
        {
            // Destroy(gameObject);
        }
    }



    private void CheckCollisions()
    {
        Collider[] contacts = Physics.OverlapSphere(transform.position, _projectileRadius, _collisionMask);

    }

    public override void UseAbility(Transform castPoint, Vector3 cursorPosition)
    {
        Debug.Log("ABILITY INSTANTIATES!");
        Instantiate(gameObject, castPoint.position, castPoint.rotation);
    }

    protected override IEnumerator HandleBehaviour(GameObject projectile, Transform castPoint, Vector3 cursorPosition)
    {
        float currentRange = 0.0f;
        yield return _baseCastTime;
        float targetDistance = Vector3.Distance(transform.position, cursorPosition);

        while (currentRange < _baseRange)
        {
            // transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            projectile.transform.Translate(Vector3.forward * Time.deltaTime);

            currentRange += Time.deltaTime;

            yield return null;
        }

        Die();
        yield break;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DED");
        HandleImpact(other.transform.rotation);
        Die();
    }

    private void HandleImpact(Quaternion rotation)
    {
        if (_impactVfx.Length > 0)
        {
            foreach (GameObject vfx in _impactVfx)
            {
                GameObject go = Instantiate(vfx, transform.position, rotation);
                Destroy(go, 1.0f);
            }
        }
    }

    private void Die()
    {

        Destroy(gameObject);
    }
}
