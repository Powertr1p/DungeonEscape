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
            GameEventsHandler.Instance.OnFlameSwordBought += ChangeDamage;
        }

        private void ChangeDamage()
        {
            DamageValue = 5;
            OnChangeDamage?.Invoke(DamageValue);
        }
        
        private void OnDisable()
        {
            GameEventsHandler.Instance.OnFlameSwordBought -= ChangeDamage;
        }
    }
}