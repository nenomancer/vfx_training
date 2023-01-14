using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image[] _abilityIcons;
    [SerializeField] private Image _ability01;

    void OnEnable()
    {
        Caster.OnCooldownUpdate += AbilityCooldown;
    }

    private void AbilityCooldown(float fillAmount)
    {
        _ability01.fillAmount = fillAmount;
    }

    private void Start()
    {
        _ability01.fillAmount = 1;
    }

}
