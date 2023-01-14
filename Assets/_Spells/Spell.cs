using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Spells/New Spell", fileName = "Spell")]
public class Spell : ScriptableObject
{


    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private string _description;

    [SerializeField] Image _abilityImage;

    [SerializeField] float _cooldown = 5.0f;
    public float Cooldown => _cooldown;

    [SerializeField]
    private bool _isCooldown = false;
    public bool IsOnCooldown => _isCooldown;

    [SerializeField] private KeyCode _shortcutKey;
    public KeyCode ShortcutKey => _shortcutKey;

    [SerializeField] private float _speed;
    public float Speed => _speed;

    [SerializeField] private GameObject _vfxMarker;
    public GameObject VFXMarker => _vfxMarker;

    [SerializeField] private SpellController _vfxEffect;
    public SpellController VFXEffect => _vfxEffect;

    public void SetIsOnCooldown(bool value) => _isCooldown = value;



}
