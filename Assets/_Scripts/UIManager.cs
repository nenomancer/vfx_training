using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image[] _abilityIcons;
    [SerializeField] private Image _ability01;

    private PlayerController player;
    private Caster playerCaster;
    private Transform abilityHolder;


    void OnEnable()
    {
        // Caster.OnCooldownUpdate += AbilityCooldown;
    }

    private void Start()
    {
        // playerCaster = FindObjectOfType<PlayerController>().GetComponent<Caster>();
        playerCaster = GameObject.Find("Player").GetComponent<Caster>();
        abilityHolder = transform.Find("AbilityHolder");
        // _ability01.fillAmount = 1;

        foreach (Transform child in abilityHolder)
        {
            child.GetComponent<Image>().color = Color.red;
        }

        foreach (GameObject ability in playerCaster.Abilities)
        {
            Debug.Log("ability");
        }

    }

    private void AbilityCooldown(float fillAmount)
    {
        // _ability01.fillAmount = fillAmount;
    }



}
