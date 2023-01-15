using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_HealthScript : MonoBehaviour
{
    public static Action OnHealthChange;

    [SerializeField] float maxHealth;
    public float MaxHealth => maxHealth;

    [SerializeField] float health;
    public float Health => health;

    [SerializeField] float damage;
    void Awake()
    {
        health = maxHealth;
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     health -= damage;
        //     if (health < 0)
        //     {
        //         health = 0;
        //     }
        //     OnHealthChange?.Invoke();

        // }
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
        OnHealthChange?.Invoke();

    }
}
