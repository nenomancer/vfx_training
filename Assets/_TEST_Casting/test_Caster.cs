using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_Caster : MonoBehaviour
{
    [SerializeField]
    Transform _castPoint;

    [SerializeField]
    GameObject _spell01;
    [SerializeField] GameObject _spell02;
    [SerializeField] GameObject _spell03;
    [SerializeField] GameObject _spell04;

    [Header("Timer Shit")]
    [SerializeField] int totalPoints;
    [SerializeField] float totalDuration;
    float totalDurationTimer;

    [SerializeField] float cycleDuration;
    float cycleDurationTimer;

    [SerializeField] float tickDuration;
    float tickDurationTimer;

    [SerializeField]
    bool isTimerOn = true;
    // Start is called before the first frame update
    void Start()
    {
        totalDurationTimer = totalDuration;
        cycleDurationTimer = cycleDuration;
        tickDurationTimer = tickDuration;
        StartCoroutine(TimerCoroutine(10.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red, 2.0f);

            }

            Rotate(hit.point);
            CastSpell();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            var firstPulse = Instantiate(_spell02, transform.position, transform.rotation);
            Destroy(firstPulse, 1.0f);
        }
        // Timer();
    }

    private IEnumerator TimerCoroutine(float duration)
    {
        bool timerIsOn = true;
        float tempCycleDuration = cycleDuration;

        while (timerIsOn)
        {
            // Debug.Log("Duration: " + (int)duration);
            if (duration > 0)
            {
                duration -= Time.deltaTime;

                if (cycleDuration > 0)
                {
                    cycleDuration -= Time.deltaTime;
                }
                else
                {
                    cycleDuration = tempCycleDuration;
                    Debug.LogError("Tick");
                }
            }
            else
            {
                timerIsOn = false;
            }
            yield return null;
        }
        Debug.Log("TIMER STOPPED!");
        yield break;
    }

    private void Timer()
    {
        if (totalDurationTimer > 0 && isTimerOn)
        {
            Debug.Log("Total Duration: " + (int)totalDurationTimer);
            totalDurationTimer -= Time.deltaTime;

        }
        else
        {
            isTimerOn = false;
            totalDuration--;
            Debug.Log("TIMER STOPPED!");
        }
    }

    public void Rotate(Vector3 hit)
    {
        Vector3 target = new Vector3(hit.x, transform.position.y, hit.z);
        transform.LookAt(target);
    }

    public void CastSpell()
    {
        var hook = Instantiate(_spell02, _castPoint.position, _castPoint.rotation);
        hook.GetComponent<test_HookSpell>().caster = transform;
    }

    public void CastSpell(Transform target)
    {
        var chainFrost = Instantiate(_spell03, _castPoint.position, _castPoint.rotation);
        chainFrost.GetComponent<test_ChainFrost>().target = target;
    }


}
