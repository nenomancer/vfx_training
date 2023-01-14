using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ability_HealingWave : MonoBehaviour
{

    [SerializeField] private int _bounces;
    [SerializeField] private float _healing, _moveSpeed, _rotSpeed, _stopDistance, _destroyTimer;
    [SerializeField] private string _tagToCheck;
    [SerializeField] private float _radius;


    private SphereCollider _collider;

    private Transform _target, _loopTransform;
    private ParticleSystem _impact, _loop;
    private bool _isDead = false;

    [SerializeField]
    private List<Transform> _possibleTargets = new List<Transform>();
    private List<Transform> _healedTargets = new List<Transform>();

    void Start()
    {
        _loop = transform.Find("Loop").GetComponent<ParticleSystem>();
        _impact = transform.Find("Impact").GetComponent<ParticleSystem>();
        _loopTransform = _loop.transform;
        _collider = GetComponent<SphereCollider>();
        _collider.radius = _radius;

        

        if (!_target && _possibleTargets.Count > 0)
        {
            _possibleTargets.Remove(_target);
            var random = Random.Range(0, _possibleTargets.Count);
            _target = _possibleTargets[random];
        }
    }

    void Update()
    {
        if (!_isDead)
        {
            if (_target)
            {
                if (_possibleTargets.Contains(_target)) _possibleTargets.Remove(_target);
                transform.LookAt(_target);
                transform.TransformDirection(Vector3.forward * _moveSpeed * Time.deltaTime);

                _loopTransform.Rotate(Vector3.up, _rotSpeed * Time.deltaTime, Space.Self);

                var distance = Vector3.Distance(transform.position, _target.position);

                if (distance < _stopDistance)
                {
                    Heal();
                }
            }
            else if (_possibleTargets.Count > 0)
            {
                _possibleTargets.Remove(_target);
                var random = Random.Range(0, _possibleTargets.Count);
                _target = _possibleTargets[random];
            }
        }
    }

    private void Heal()
    {
        // _target.GetComponent<Unit>().TakeDamage(_healing);
        _healedTargets.Add(_target);
        _possibleTargets.Remove(_target);
        _impact.Play();

        if (_possibleTargets.Count > 0)
        {
            var random = Random.Range(0, _possibleTargets.Count);
            _target = _possibleTargets[random];
            _bounces -= 1;
            if (_bounces <= 0) FinishWave();
        }
    }

    private void FinishWave()
    {
        _isDead = true;
        var loopEmission = _loop.emission;
        loopEmission.rateOverTime = 0;
        Destroy(gameObject, _destroyTimer);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TARGET ADDED");
        if (other.GetComponent<test_Target>() != null
            && !_possibleTargets.Contains(other.transform)
            && _target != other.transform
            && !_isDead
            && !_healedTargets.Contains(other.transform))
        {
            _possibleTargets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<test_Target>() != null && !_isDead)
        {
            _possibleTargets.Remove(other.transform);
        }
    }
}
