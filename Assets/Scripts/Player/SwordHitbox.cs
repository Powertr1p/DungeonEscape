using System;
using Interfaces;
using UnityEngine;

namespace Player
{
  [RequireComponent(typeof(Collider2D))]
  public class SwordHitbox : MonoBehaviour
  {
    private int _damage;
    
    private void Awake()
    {
      _damage = GetComponentInParent<DamageDealer>().WeaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
      if (other.gameObject.TryGetComponent(out IDamagable enemy)) 
      { 
        enemy.ApplyDamage(_damage);
      }
    }
  }
}
