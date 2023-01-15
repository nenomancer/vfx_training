using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_HealthbarScript : MonoBehaviour
{
    private Camera _cam;
    private test_HealthScript playa;

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
        test_HealthScript.OnHealthChange += ResetFillTimers;
    }

    void Start()
    {
        _cam = Camera.main;
        playa = GetComponentInParent<test_HealthScript>();
        material = GetComponentInChildren<Renderer>().material;

        material.SetFloat("_HealthbarMaxHealth", playa.MaxHealth);
        material.SetFloat("_HealthbarCurrentHealth", playa.Health);
        material.SetFloat("_TrailHealth", playa.Health);
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
                    playa.Health,
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
                    playa.Health,
                    Time.deltaTime * (fillRate * 3)
                )
            );
            // trailEffectTimer = Mathf.Sin(Time.deltaTime * 100.0f);
            Debug.Log(trailEffectTimer);
            trailEffectTimer -= Time.deltaTime * 20.0f;
        }
    }

    private void OnDisable()
    {
        test_HealthScript.OnHealthChange -= ResetFillTimers;
    }

    void ResetFillTimers()
    {
        trailEffectTimer = 1.0f;
        fillDelayTimer = fillDelay;
    }
}
