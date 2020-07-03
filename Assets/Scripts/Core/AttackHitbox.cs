using System.Collections;
using Interfaces;
using UnityEngine;

namespace Core
{
  [RequireComponent(typeof(Collider2D))]
  public class AttackHitbox : MonoBehaviour
  {
    protected int Damage;
    private bool _canDamage = true;

    private void Awake()
    {
      Init();
    }

    protected virtual void Init()
    {
      Damage = GetComponentInParent<DamageDealer>().GetDamageValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.TryGetComponent(out IDamagable entity))
      {
        if (!_canDamage)
          return;
        
        _canDamage = false;
        entity.ApplyDamage(Damage);
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
