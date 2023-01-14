using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_ChainFrost : MonoBehaviour
{

    public bool Debug;

    public int bounces;
    public float moveSpeed, rotSpeed, distance, radius;
    public Transform target;

    private ParticleSystem loop, impact;
    private bool isDead = false;
    private List<Transform> possibleTargets = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {

        loop = transform.Find("Loop").GetComponent<ParticleSystem>();
        impact = transform.Find("Loop/Impact").GetComponent<ParticleSystem>();

        if (!target && possibleTargets.Count > 0)
        {
            possibleTargets.Remove(target);
            var random = Random.Range(0, possibleTargets.Count);
            target = possibleTargets[random];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (target)
            {
                if (possibleTargets.Contains(target)) possibleTargets.Remove(target);

                var dist = Vector3.Distance(transform.position, target.position);

                // Rotate 
                var lookPos = target.position - transform.position;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed);

                // Move
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

                if (dist < distance)
                {
                    StartChainFrost();
                }
            }
            else if (possibleTargets.Count > 0)
            {
                possibleTargets.Remove(target);
                var random = Random.Range(0, possibleTargets.Count);
                target = possibleTargets[random];
            }
        }
    }

    private void StartChainFrost()
    {
        possibleTargets.Remove(target);
        var random = Random.Range(0, possibleTargets.Count);
        target = possibleTargets[random];

        impact.Play();
        bounces -= 1;

        if (bounces <= 0) FinishChainFrost();

        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in objectsInRange)
        {
            test_Target target = col.GetComponent<test_Target>();

            if (target != null)
            {
                target.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }

    private void FinishChainFrost()
    {
        isDead = true;
        var loopEmi = loop.emission;
        loopEmi.rateOverTime = 0;
        Destroy(gameObject, 2);
    }

    private void OnTriggerStay(Collider other)
    {
        test_Target possibleTarget = other.GetComponent<test_Target>();

        if (possibleTarget != null && !possibleTargets.Contains(possibleTarget.transform) && target != possibleTarget.transform && !isDead)
        {
            possibleTargets.Add(possibleTarget.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        test_Target possibleTarget = other.GetComponent<test_Target>();

        if (possibleTarget != null && !isDead)
        {
            possibleTargets.Remove(possibleTarget.transform);
        }
    }
    private void OnDrawGizmos() {
        if (Debug) 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
