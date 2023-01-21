using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Caster))]
public class PlayerController : MonoBehaviour
{

    private Caster _caster;
    private NavMeshAgent _agent;

    private Animator _animator;
    private float _animationSmoothTime = 0.1f;

    private Camera _cam;
    private Ray _ray;
    private RaycastHit _hit;
    private Vector3 _cursorPosition;

    void Start()
    {
        // _vfxMarker = Instantiate(_spell.VFXMarker, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity, transform);
        // _vfxMarker.SetActive(false);
        _caster = GetComponent<Caster>();
        _agent = _caster.GetComponent<NavMeshAgent>();

        _animator = GetComponent<Animator>();
        _cam = Camera.main;
    }

    async void Update()
    {

        // foreach (Caster.PlayerAbility ability in _caster.Abilities)
        // {
        //     if (Input.GetKeyDown(ability._shortcutKey))
        //     {
        //         _caster.SelectAbility(ability._ability);
        //     }
        // }

        if (Input.GetMouseButtonDown(1))
        {
            _cursorPosition = GetCursorPosition();

            await _caster.Rotate(_cursorPosition);
            _caster.CastSpell();
            // _cursorPosition = GetCursorPosition();
            // await _caster.Rotate(_cursorPosition);
            // _caster.Move(_cursorPosition);

            // _caster.DeselectAbility();
        }

        if (Input.GetMouseButtonDown(0))
        {
            _cursorPosition = GetCursorPosition();
            _caster.Rotate(_cursorPosition);
            _caster.Move(_cursorPosition);
            Debug.Log("NOW WE CAN MOF");
            // _caster.CastSpell();
            // _caster.DeselectAbility();
        }

        float speed = _agent.velocity.sqrMagnitude / _agent.speed;
        _animator.SetFloat("Speed", speed, _animationSmoothTime, Time.deltaTime);
    }

    protected Vector3 GetCursorPosition()
    {
        _ray = _cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out _hit);

        return _hit.point;
    }
}
