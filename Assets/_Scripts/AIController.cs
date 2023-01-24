using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Unit))]
public class AIController : MonoBehaviour
{

    private Unit _unit;
    private NavMeshAgent _agent;

    private Vector3 _targetPosition;

    [Header("Patrol Settings")]
    [SerializeField] private float _patrolRadius = 3.0f;
    [SerializeField] private float _patrolInterval = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _targetPosition = GetRandomPosition();
        _agent.SetDestination(_targetPosition);

    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, _targetPosition, Color.red);
        // _agent.SetDestination(_targetPosition);
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _targetPosition = GetRandomPosition();
            _agent.SetDestination(_targetPosition);
        }
    }
    private IEnumerator AIPatrol()
    {

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _targetPosition = GetRandomPosition();
            _agent.SetDestination(_targetPosition);
            yield return null;
        }

        yield return null;
    }

    private Vector3 GetRandomPosition()
    {
        return transform.position + Random.onUnitSphere * _patrolRadius;

    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("KJKUR");
    }


}
