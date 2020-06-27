using System;
using Core;
using UnityEngine;

namespace Player
{
    public class PlayerDamageDealer : DamageDealer
    {
        public event Action<int> OnChangeDamage;
        
        private void Start()
        {
            GameManager.Instance.OnFlameSwordBought += ChangeDamage;
        }

        private void ChangeDamage()
        {
            DamageValue = 5;
            OnChangeDamage?.Invoke(DamageValue);
        }
        
        private void OnDisable()
        {
            GameManager.Instance.OnFlameSwordBought -= ChangeDamage;
        }
    }
}