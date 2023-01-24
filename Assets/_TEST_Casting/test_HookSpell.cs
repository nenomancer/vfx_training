using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.AI;

public class test_HookSpell : MonoBehaviour
{
    [SerializeField] string[] tagsToCheck;
    [SerializeField] float speed, returnSpeed, range, stopRange, hitDamage, dragDamage, dragDamageTimer;

    [HideInInspector] public Caster caster;
    [HideInInspector] public Transform castPoint;
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

            line.SetPosition(0, castPoint.position);
            line.SetPosition(1, transform.position);

            if (hasCollided)
            {
                transform.LookAt(castPoint);

                var dist = Vector3.Distance(transform.position, castPoint.position);
                if (dist < stopRange)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                caster.enableMovement = false;
                var dist = Vector3.Distance(transform.position, castPoint.position);
                if (dist > range)
                {
                    caster.enableMovement = true;
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
