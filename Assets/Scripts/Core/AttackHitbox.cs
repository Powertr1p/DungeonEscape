using System.Collections;
using Interfaces;
using Player;
using UnityEngine;

namespace Core
{
  [RequireComponent(typeof(Collider2D))]
  public class AttackHitbox : MonoBehaviour
  {
    private int _damage;
    private bool _canDamage = true;

    private void Awake()
    {
      _damage = GetComponentInParent<DamageDealer>().GetDamageValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
      if (other.gameObject.TryGetComponent(out IDamagable entity))
      {
        if (!_canDamage) return;

        _canDamage = false;
        entity.ApplyDamage(_damage);
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
