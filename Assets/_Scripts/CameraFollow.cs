using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _cameraOffset;


    [Range(0.01f, 1.0f)]
    [SerializeField] private float _smoothness = 0.5f;

    void Start()
    {
        _cameraOffset = transform.position - _target.transform.position;
    }

    void Update()
    {
        Vector3 targetPosition = _target.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, targetPosition, _smoothness);
    }
}
