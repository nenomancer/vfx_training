using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellmaneSpell : MonoBehaviour
{
    [SerializeField] private float _radius;
    private Collider[] _nearbyUnits;
    private List<Unit> _enemies;

    private bool _isDead = false;

    [SerializeField] private float _duration;
    [SerializeField] private float _damageTickInterval;
    private float _damageTick, _time, _savedDuration;


    [SerializeField] private float _damage;


    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one * _radius;
        _savedDuration = _duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (_duration >= 0)
        {
            _duration -= Time.deltaTime;


            if (_damageTick >= 0) _damageTick -= Time.deltaTime;
            else Tick();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Tick()
    {
        _damageTick = _damageTickInterval;
        _nearbyUnits = Physics.OverlapSphere(transform.position, _radius / 2);
        foreach (Collider collider in _nearbyUnits)
        {
            Unit unit = collider.GetComponent<Unit>();
            IDamagable damagable = collider.GetComponent<IDamagable>();
            if (unit != null)
            {
                Debug.Log("hit");
                unit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                damagable.TakeDamage(_damage);
            }
            else
            {
                Debug.Log("NOT HIT");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTERED ME");
    }
}
