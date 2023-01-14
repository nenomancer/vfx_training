using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(float damage);
    public void DamageOverTime(float damage, float duration, float interval);
    public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection);
}
