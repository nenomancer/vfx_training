using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_RangedSpell : MonoBehaviour
{

    [SerializeField] float _speed;
    [SerializeField] float _range;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (_range > 0)
        {
            _range -= Time.deltaTime;
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HITTED");
        test_Target target = other.transform.GetComponent<test_Target>();
        if (target != null) Destroy(other.gameObject);
    }
}
