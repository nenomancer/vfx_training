using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class test_HookSpell : MonoBehaviour
{
    [SerializeField] string[] tagsToCheck;
    [SerializeField] float speed, returnSpeed, range, stopRange, hitDamage, dragDamage, dragDamageTimer;

    [HideInInspector] public Transform caster;
    [HideInInspector] public Unit collidedWith;
    private LineRenderer line;
    private bool hasCollided;
    void Start()
    {
        line = transform.Find("Line").GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (caster)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

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


            if (collidedWith)
            {
                Vector3 targetPosition = new Vector3(
                    transform.position.x,
                    collidedWith.transform.position.y,
                    transform.position.z);

                collidedWith.transform.position = targetPosition;
                if (dragDamageTimer > 0)
                {
                    dragDamageTimer -= Time.deltaTime;
                }
                else
                {
                    collidedWith.TakeDamage(dragDamage);
                    dragDamageTimer = 1f;
                }

            }
        }
        else Destroy(gameObject);

    }


    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Unit>();
        if (!hasCollided && enemy != null)
        {
            enemy.TakeDamage(hitDamage);
            Collision(enemy);
        }
    }

    private void Collision(Unit col)
    {
        speed = returnSpeed;
        hasCollided = true;
        dragDamageTimer = 1f;

        if (col)
        {
            Vector3 targetPosition = new Vector3(
                col.transform.position.x,
                transform.position.y,
                col.transform.position.z);

            transform.position = targetPosition;
            collidedWith = col;
        }
    }
}
