using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ability_InkSwell : MonoBehaviour
{
    [SerializeField] private float _value, _radius, _duration, _maximumValue, _tickInterval, _damagePerTick, _valueIncreasePerTick, _endFXDestroyTime;

    [SerializeField] private Transform _transformToFollow;

    private Transform _areaVisual, _areaOfDamage;
    private ParticleSystem _endFX;

    private float _tick, _t, _savedDuration;
    private Vector3 _endScale;

    // Start is called before the first frame update
    void Start()
    {
        _endScale = new Vector3(_radius * 2, 0.05f, _radius * 2);
        _savedDuration = _duration;

        _areaVisual = transform.Find("AreaVisual");
        _areaOfDamage = transform.Find("AreaOfDamage");
        _endFX = transform.Find("EndFX").GetComponent<ParticleSystem>();


        _endFX.transform.localScale = _endScale;
        _areaOfDamage.localScale = _endScale;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = _transformToFollow.position;

        if (_duration >= 0)
        {
            _duration -= Time.deltaTime;
            _t += Time.deltaTime / _savedDuration;
            _areaVisual.localScale = Vector3.Lerp(Vector3.zero, _endScale, _t);

            if (_tick >= 0)
            {
                _tick -= Time.deltaTime;
            }
            else Tick();
        }

        else Explode();
    }

    private void Explode()
    {
        _endFX.Play();
        Debug.Log("BOOM!");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider collider in hitColliders)
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            if (damagable != null)
            {

                damagable.TakeDamage(_damagePerTick);
            }
        }
        Destroy(gameObject);
    }

    private void Tick()
    {
        _tick = _tickInterval;
        float addition = 0;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider collider in hitColliders)
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                addition += _valueIncreasePerTick;

                damagable.TakeDamage(_damagePerTick);
            }
        }

        _value += addition;
    }
}
