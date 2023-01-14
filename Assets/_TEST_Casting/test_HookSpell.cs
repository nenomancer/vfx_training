using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class test_HookSpell : MonoBehaviour
{
    [SerializeField] string[] tagsToCheck;
    [SerializeField] float speed, returnSpeed, range, stopRange;

    [HideInInspector] public Transform caster, collidedWith;
    private LineRenderer line;
    private bool hasCollided;
    // Start is called before the first frame update
    void Start()
    {
        line = transform.Find("Line").GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (caster)
        {
            line.SetPosition(0, caster.position);
            line.SetPosition(1, transform.position);

            if (hasCollided)
            {
                transform.LookAt(caster);

                var dist = Vector3.Distance(transform.position, caster.position);
                if (dist < stopRange)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                var dist = Vector3.Distance(transform.position, caster.position);
                if (dist > range)
                {
                    Collision(null);
                }
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (collidedWith)
                collidedWith.transform.position = transform.position;
        }
        else Destroy(gameObject);

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Gottem");
        var enemy = other.GetComponent<test_Target>();
        if (!hasCollided && enemy != null)
        {
            Destroy(enemy.gameObject);
            Collision(other.transform);
        }
    }

    private void Collision(Transform col)
    {
        speed = returnSpeed;
        hasCollided = true;

        if (col)
        {
            transform.position = col.position;
            collidedWith = col;
        }
    }
}
