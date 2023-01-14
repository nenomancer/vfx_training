using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [Header("Basic Information")]
    [SerializeField] Image _abilityImage;

    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private string _description;

    [SerializeField] float _cooldown = 5.0f;
    public float Cooldown => _cooldown;

    [SerializeField]
    private bool _isCooldown = false;
    public bool IsOnCooldown => _isCooldown;

    [Header("Base Values")]
    [SerializeField] protected float _baseDamage;
    [SerializeField] protected float _baseCost;
    [SerializeField] protected float _baseCooldown;
    [SerializeField] protected float _baseRange;
    public float BaseRange => _baseRange;
    [SerializeField] protected float _baseDuration;
    [SerializeField] protected float _baseCastTime;

    [Header("Ability VFX")]
    [SerializeField] protected GameObject[] _vfxMarkers;
    [SerializeField] protected GameObject[] _characterVfx;
    [SerializeField] protected GameObject[] _impactVfx;

    [SerializeField] protected LayerMask _collisionMask;

    public virtual void UseAbility(Transform castPoint, Vector3 cursorPosition)
    {
        Debug.LogError("This method should be overwritten by the inherited class!");
    }

    protected virtual IEnumerator HandleBehaviour(Transform castPoint)
    {
        Debug.LogError("Add a behaviour for this ability");
        yield break;
    }

     protected virtual IEnumerator HandleBehaviour(GameObject projectile, Transform castPoint, Vector3 cursorPosition)
    {
        Debug.LogError("Add a behaviour for this ability");
        yield break;
    }






    public virtual void UseAbility(Transform castPoint)
    {
        Debug.LogError("This method should be overwritten by the inherited class!");
    }

    public void SetIsOnCooldown(bool value) => _isCooldown = value;
}
