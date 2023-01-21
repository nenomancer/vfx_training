using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Caster : Unit
{
    [SerializeField]
    Transform _castPoint;

    [SerializeField] GameObject _spell01;
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
    async void Start()
    {
        totalDurationTimer = totalDuration;
        cycleDurationTimer = cycleDuration;
        tickDurationTimer = tickDuration;
        // StartCoroutine(TimerCoroutine(10.0f));

        // await Timer();
        Debug.Log("FINSIHED IN " + Time.time);

    }

    private async Task Timer()
    {
        float duration = 5.0f;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            await Task.Yield();
            Debug.Log("duration: " + duration);
        }

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
                }
            }
            else
            {
                timerIsOn = false;
            }
            yield return null;
        }
        yield break;
    }

    // private void Timer()
    // {
    //     if (totalDurationTimer > 0 && isTimerOn)
    //     {
    //         Debug.Log("Total Duration: " + (int)totalDurationTimer);
    //         totalDurationTimer -= Time.deltaTime;

    //     }
    //     else
    //     {
    //         isTimerOn = false;
    //         totalDuration--;
    //         Debug.Log("TIMER STOPPED!");
    //     }
    // }

    public void CastSpell()
    {
        var hook = Instantiate(_spell01, _castPoint.position, _castPoint.rotation);
        hook.GetComponent<test_HookSpell>().caster = _castPoint;
    }

    private void CastSpell(Transform target)
    {
        var chainFrost = Instantiate(_spell03, _castPoint.position, _castPoint.rotation);
        chainFrost.GetComponent<test_ChainFrost>().target = target;
    }

}
