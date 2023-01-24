using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Caster : Unit
{
    [SerializeField]
    Transform _castPoint;

    [SerializeField]
    private GameObject[] abilities;
    public GameObject[] Abilities;

    [SerializeField] GameObject _spell01;
    [SerializeField] GameObject _spell02;
    [SerializeField] GameObject _spell03;
    [SerializeField] GameObject _spell04;

    private GameObject selectedAbility;



    void Start()
    {

        // StartCoroutine(TimerCoroutine(10.0f));

        // await Timer();
        Debug.Log("FINSIHED IN " + Time.time);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedAbility = abilities[0];
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            selectedAbility = abilities[1];
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedAbility = abilities[2];
        }
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


    public void CastSpell()
    {
        if (!selectedAbility) return;
        var hook = Instantiate(selectedAbility, _castPoint.position, _castPoint.rotation);
        hook.GetComponent<test_HookSpell>().caster = this;
        hook.GetComponent<test_HookSpell>().castPoint = _castPoint;
        selectedAbility = null;
    }

    private void CastSpell(Transform target)
    {
        var chainFrost = Instantiate(_spell03, _castPoint.position, _castPoint.rotation);
        chainFrost.GetComponent<test_ChainFrost>().target = target;
    }

}
