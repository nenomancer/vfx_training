using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_DeathPulse : MonoBehaviour
{
    public Transform target;
    public int limit;
    public float speed;
    public float radius;
    public float impactDistance;
    public float destroyDelay;
    public float swaySpeed;
    public float swaySize;
    public bool isFirst;

    Vector3 startPosition;
    GameObject visuals;


    void Start()
    {
        if (!isFirst)
        {
            startPosition = transform.position;
            visuals = transform.Find("Visuals").gameObject;
            visuals.SetActive(true);

            transform.Find("SpawnFX").gameObject.SetActive(false);
        }
        else
            SpawnPulses();

    }



    void Update()
    {
        if (target)
        {
            transform.LookAt(target);
            startPosition += transform.forward * speed * Time.deltaTime;
            transform.position = startPosition + transform.right * Mathf.Sin(swaySpeed * Time.deltaTime) * swaySize;

            var distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget < impactDistance)
            {
                Debug.Log("DAMAGE");

                visuals.SetActive(false);
                target = null;
                Destroy(gameObject, destroyDelay);
            }
        }


    }

    void SpawnPulses()
    {
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, radius);

        Collider tempCol;

        // Shuffle
        for (int i = 0; i < nearbyEnemies.Length; i++)
        {
            int random = Random.Range(0, nearbyEnemies.Length);
            tempCol = nearbyEnemies[random];
            nearbyEnemies[random] = nearbyEnemies[i];
            nearbyEnemies[i] = tempCol;
        }

        foreach (Collider col in nearbyEnemies)
        {
            test_Target enemy = col.GetComponent<test_Target>();

            if (enemy != null && enemy.tag != "Player")
            {
                var pulse = Instantiate(gameObject, transform.position, Quaternion.identity);
                pulse.GetComponent<test_DeathPulse>().isFirst = false;
                pulse.GetComponent<test_DeathPulse>().target = enemy.transform;

                limit--;
                if (limit <= 0) break;

            }
        }
    }
}
