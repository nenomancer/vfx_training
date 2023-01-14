using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour, IDamagable
{
    public event Action OnDeath;

    [SerializeField] private float _rotateSpeed;
    private float _rotateVelocity;

    [SerializeField]
    protected float health;
    [SerializeField]
    protected float moveSpeed;
    protected bool isDead;
    protected NavMeshAgent agent;

    private Coroutine _rotationCoroutine;
    private Coroutine _movementCoroutine;
    // Maybe make some array of states/effects for the unit, for example burn, slow, poison, etc...
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.acceleration = 100.0f;
    }


    #region Rotation
    public void Rotate(Vector3 targetPosition)
    {
        if (_rotationCoroutine != null)
            StopCoroutine(_rotationCoroutine);

        _rotationCoroutine = StartCoroutine(HandleRotation(targetPosition));
    }

    public IEnumerator HandleRotation(Vector3 targetPosition)
    {
        Quaternion lookRotation = Quaternion.LookRotation(targetPosition - transform.position);
        float rotationY = transform.eulerAngles.y;

        while (Mathf.Abs(rotationY - lookRotation.eulerAngles.y) > 0.1f)
        {
            rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
             lookRotation.eulerAngles.y,
             ref _rotateVelocity,
             _rotateSpeed * (Time.deltaTime * 5));
            transform.eulerAngles = new Vector3(0, rotationY, 0);

            yield return null;
        }
        yield break;
    }

    #endregion 

    #region Movement
    public void Move(Vector3 targetPosition)
    {
        StartCoroutine(HandleMovement(targetPosition));
    }

    public void Move(Vector3 targetPosition, float stoppingDistance)
    {
        StartCoroutine(HandleMovement(targetPosition, stoppingDistance));
    }

    public IEnumerator HandleMovement(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
        float targetDistance = (targetPosition - transform.position).magnitude;
        while (transform.position != agent.destination)
        {
            yield return null;
        }
        yield break;
    }

    public IEnumerator HandleMovement(Vector3 targetPosition, float stoppingDistance)
    {
        Vector3 direction = targetPosition - transform.position;
        float distance = direction.magnitude;
        UnityEngine.Debug.Log("DISTANCE: " + distance);
        Vector3 targetDirection = new Vector3(direction.x - stoppingDistance, direction.y, direction.z - stoppingDistance);
        agent.SetDestination(targetDirection);
        agent.stoppingDistance = stoppingDistance;
        float targetDistance = direction.magnitude - stoppingDistance;
        while (direction.magnitude > targetDistance)
        {
            direction = targetPosition - transform.position;
            UnityEngine.Debug.Log("Moving: " + direction.magnitude);
            yield return null;
        }
        UnityEngine.Debug.Log("Stopped");
        yield break;
    }


    public void StopMovement()
    {
        agent.SetDestination(transform.position);
    }
    #endregion

    #region Damage

    public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        UnityEngine.Debug.Log("HIT");
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead) Die();
    }

    public void DamageOverTime(float damage, float duration, float damageInterval)
    {
        StartCoroutine(DoT(damage, duration, damageInterval));
        UnityEngine.Debug.Log("DAMAGED BOI");

    }

    private IEnumerator DoT(float damage, float duration, float interval)
    {
        Stopwatch timer = new Stopwatch();
        UnityEngine.Debug.Log("DAMAGING COMMENCED");
        // float timer = 0.0f;
        timer.Start();
        while (timer.Elapsed.Seconds < duration)
        {
            UnityEngine.Debug.Log($"timer is on {timer.Elapsed.Seconds}");
            TakeDamage(damage);
            yield return new WaitForSecondsRealtime(interval);

        }

        timer.Stop();
        timer.Reset();
        yield break;
    }

    #endregion

    private void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }
}
