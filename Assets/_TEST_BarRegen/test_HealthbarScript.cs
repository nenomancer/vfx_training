using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_HealthbarScript : MonoBehaviour
{

    private Camera _cam;
    private test_HealthScript playa;

    private Image healthbar;

    [SerializeField]
    RectTransform healthBarRect;
    private Slider topSlider;
    private Slider bottomSlider;

    private GameObject healthBarMesh;
    private Material material;

    [Header("Fill Settings")]
    [SerializeField] float fillRate;
    [SerializeField] float fillDelay;

    float fillDelayTimer;
    private float fillAmount;

    private void OnEnable()
    {
        test_HealthScript.OnHealthChange += ResetFillTimers;
    }
    void Start()
    {
        _cam = Camera.main;
        playa = GetComponentInParent<test_HealthScript>();
        // healthBarMesh = GetComponent
        material = GetComponentInChildren<Renderer>().material;

        // topSlider = transform.Find("Top").GetComponent<Slider>();
        // bottomSlider = transform.Find("Bottom").GetComponent<Slider>();

        material.SetFloat("_CurrentHealth", playa.Health);
        material.SetFloat("_MaxHealth", playa.MaxHealth);
        // material = healthBarRect.GetComponentInChildren<Image>().material;
    }

    void Update()
    {
        // material.SetFloat("_CurrentHealth", playa.Health);
        transform.rotation = _cam.transform.rotation;
        // fillAmount = playa.Health / playa.MaxHealth;

        HandleBarFill();
    }

    private void HandleBarFill()
    {
        if (fillDelayTimer > 0)
        {
            fillDelayTimer -= Time.deltaTime;
            float current = material.GetFloat("_CurrentHealth");
            Debug.Log(current);
            // material.SetFloat("_CurrentHealth", playa.Health);
            material.SetFloat("_CurrentHealth", Mathf.Lerp(material.GetFloat("_CurrentHealth"), playa.Health, Time.deltaTime));

            // topSlider.value = Mathf.Lerp(topSlider.value, fillAmount, Time.deltaTime * fillRate);
        }
        else
        {
            bottomSlider.value = Mathf.Lerp(bottomSlider.value, fillAmount, Time.deltaTime * fillRate);
        }
    }

    private void OnDisable()
    {
        test_HealthScript.OnHealthChange -= ResetFillTimers;
    }

    void ResetFillTimers()
    {
        fillDelayTimer = fillDelay;

    }
}
