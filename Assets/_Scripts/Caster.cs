using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : Unit
{
    [SerializeField] private Transform _castPoint;
    public Transform CastPoint => _castPoint;

    [SerializeField] private Spell[] _spells;
    public Spell[] Spells => _spells;

    [Serializable]
    public class PlayerAbility
    {
        public Ranged _ability;
        public KeyCode _shortcutKey;
    }

    [SerializeField] GameObject _inkSwell;
    [SerializeField] GameObject _healingWave;

    [SerializeField] private PlayerAbility[] _abilities;
    public PlayerAbility[] Abilities => _abilities;

    private Spell _currentSpell;


    private Ranged _currentAbility;
    public Ranged CurrentAbility => _currentAbility;

    private GameObject _currentAbilityGO;
    private CapsuleCollider _abilityRangeCollider;

    private Coroutine _castCoroutine;

    private bool _spellIsActive = false;
    public bool SpellIsActive
    {
        get
        {
            return _spellIsActive;
        }
        set
        {
            _spellIsActive = value;
        }
    }

    public static event Action<float> OnCooldownUpdate;

    private void Start()
    {
        foreach (Spell spell in _spells)
            spell.SetIsOnCooldown(false);


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            var ink = Instantiate(_inkSwell, transform.position, transform.rotation, transform);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            Physics.Raycast(_ray, out _hit);

            var wave = Instantiate(_healingWave, _hit.point, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        if (_currentAbility != null)
        {
            Gizmos.color = new Color(.4f, .7f, .2f, .1f);
            Gizmos.DrawSphere(transform.position, _currentAbility.BaseRange);
        }
    }

    public void ActivateSpell(Spell spell)
    {
        _currentSpell = spell;
        if (_currentSpell.IsOnCooldown) return;
        else _spellIsActive = true;
    }

    public void SelectAbility(Ranged ability)
    {
        // send a message that an ability has been selected, sending the ability index
        _currentAbility = ability;
        Debug.Log("Cyrrebt avukuty: " + _currentAbility);
        if (_currentAbility.IsOnCooldown) return;
        else _spellIsActive = true;
    }

    public void DeselectAbility()
    {
        _spellIsActive = false;
        _currentAbility = null;
    }

    public void DeactivateSpell()
    {
        _spellIsActive = false;
        _currentSpell = null;
    }

    public void HandleAbility(Vector3 cursorPosition)
    {
        Move(cursorPosition, _currentAbility.BaseRange);
        if (_castCoroutine != null)
            StopCoroutine(_castCoroutine);
        _castCoroutine = StartCoroutine(CastAbility(cursorPosition));
    }

    public IEnumerator CastAbility(Vector3 cursorPosition)
    {

        yield return HandleMovement(cursorPosition, _currentAbility.BaseRange);
        _currentAbility.UseAbility(_castPoint, cursorPosition);
        Debug.Log("SHOTT");

        yield break;
    }

    public void CastSpell()
    {
        if (!_spellIsActive) return;
        // check spell type with a for loop


        Instantiate(_currentSpell.VFXEffect, _castPoint.position, _castPoint.rotation);
        _spellIsActive = false;

        _currentSpell.SetIsOnCooldown(true);
        StartCoroutine(SpellCooldown(_currentSpell));
    }

    private IEnumerator SpellCooldown(Spell spell)
    {
        Debug.Log($"Starting cooldown for {spell.Name}");
        float cooldown = 0.0f;

        while (cooldown <= spell.Cooldown)
        {
            cooldown += Time.deltaTime;
            OnCooldownUpdate?.Invoke(cooldown / spell.Cooldown);

            yield return null;
        }

        Debug.Log($"Cooldown for {spell.Name} ended");
        spell.SetIsOnCooldown(false);

        yield break;
    }
}
