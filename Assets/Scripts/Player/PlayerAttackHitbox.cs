using System;
using Core;
using UnityEngine;

namespace Player
{
    public class PlayerAttackHitbox : AttackHitbox
    {
        [SerializeField] private PlayerDamageDealer _damageDealer;

        private void OnEnable()
        {
            _damageDealer.OnChangeDamage += ChangeDamage;
        }

        protected override void Init()
        {
            Damage = _damageDealer.GetDamageValue;
        }

        private void ChangeDamage(int damage)
        {
            Damage = damage;
        }
        
        private void OnDisable()
        {
            _damageDealer.OnChangeDamage -= ChangeDamage;
        }
    }
}