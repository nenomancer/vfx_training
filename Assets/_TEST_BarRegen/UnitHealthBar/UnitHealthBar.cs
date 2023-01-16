using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthBar : MonoBehaviour
{
    private Camera _cam;
    private Unit unit;

    private Material material;

    [Header("Fill Settings")]
    [SerializeField]
    float fillRate;

    [SerializeField]
    float fillDelay;
    float fillDelayTimer;
    private float fillAmount;

    [SerializeField]
    private float trailEffectTimer;

    private void OnEnable()
    {
        Unit.OnHealthChange += ResetFillTimers;
    }

    void Start()
    {
        _cam = Camera.main;
        unit = GetComponentInParent<Unit>();
        Debug.Log(unit.MaxHealth);
        material = GetComponentInChildren<Renderer>().material;

        material.SetFloat("_HealthbarMaxHealth", unit.MaxHealth);
        material.SetFloat("_HealthbarCurrentHealth", unit.Health);
        material.SetFloat("_TrailHealth", unit.Health);
    }

    void Update()
    {
        transform.rotation = _cam.transform.rotation;
        material.SetFloat("_TrailEffectTimer", trailEffectTimer);
        HandleBarFill();
    }

    private void HandleBarFill()
    {
        if (fillDelayTimer > 0)
        {
            fillDelayTimer -= Time.deltaTime;
            material.SetFloat(
                "_HealthbarCurrentHealth",
                Mathf.Lerp(
                    material.GetFloat("_HealthbarCurrentHealth"),
                    unit.Health,
                    Time.deltaTime * fillRate
                )
            );

        }
        else
        {
            material.SetFloat(
                "_TrailHealth",
                Mathf.Lerp(
                    material.GetFloat("_TrailHealth"),
                    unit.Health,
                    Time.deltaTime * (fillRate * 3)
                )
            );
            // trailEffectTimer = Mathf.Sin(Time.deltaTime * 100.0f);
            trailEffectTimer -= Time.deltaTime * 20.0f;
        }
    }

    private void OnDisable()
    {
        Unit.OnHealthChange -= ResetFillTimers;
    }

    void ResetFillTimers()
    {
        trailEffectTimer = 1.0f;
        fillDelayTimer = fillDelay;
    }
}
