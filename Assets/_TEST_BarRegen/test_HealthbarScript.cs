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

        material.SetFloat("_MaxHealth", playa.MaxHealth);
        material.SetFloat("_CurrentHealth", playa.Health);
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
                "_CurrentHealth",
                Mathf.Lerp(
                    material.GetFloat("_CurrentHealth"),
                    playa.Health,
                    Time.deltaTime * fillRate
                )
            );
            Debug.Log(
                Mathf.Abs(material.GetFloat("_CurrentHealth") / playa.Health) <= Mathf.Epsilon + 1
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
            trailEffectTimer -= Time.deltaTime * 100.0f;
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
