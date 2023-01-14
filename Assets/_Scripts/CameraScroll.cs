using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    private Camera _cam;
    private float _camFOV;
    [SerializeField] private float _zoomSpeed;

    private float _mouseScrollInput;

    void Start()
    {
        _cam = Camera.main;
        _camFOV = _cam.fieldOfView;
    }

    void Update()
    {
        _mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");

        _camFOV -= _mouseScrollInput * _zoomSpeed;
        _camFOV = Mathf.Clamp(_camFOV, 20, 60);

        _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _camFOV, _zoomSpeed);
    }
}
