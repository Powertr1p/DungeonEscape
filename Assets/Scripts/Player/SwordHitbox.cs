using System;
using System.Collections;
using Interfaces;
using UnityEngine;

namespace Player
{
  [RequireComponent(typeof(Collider2D))]
  public class SwordHitbox : MonoBehaviour
  {
    private int _damage;
    private bool _canDamage = true;

    private void Awake()
    {
      _damage = GetComponentInParent<DamageDealer>().WeaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
      if (other.gameObject.TryGetComponent(out IDamagable enemy))
      {
        if (!_canDamage) return;

        _canDamage = false;
        enemy.ApplyDamage(_damage);
        StartCoroutine(ResetDamageCooldown());
      }
    }

    private IEnumerator ResetDamageCooldown()
    {
      yield return new WaitForSeconds(0.5f);
      _canDamage = true;
    }

  }
}
