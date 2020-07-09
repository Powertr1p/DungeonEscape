using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IDamagable
    {
        public event Action<int> DamageTaken;
        public event Action Died;

        private int _livesRemaining = 4;

        public bool IsAlive => _livesRemaining > 0;
        public int DiamondsCount { get; set; } = 600;

        public void ApplyDamage(int damage)
        {
            if (!IsAlive) return;

            _livesRemaining -= damage;
            DamageTaken?.Invoke(_livesRemaining);

            if (_livesRemaining <= 0)
                Died?.Invoke();
        }
    }
}