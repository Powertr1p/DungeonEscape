using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IDamagable
    {
        public event Action DiamondsCountUpdated;
        public event Action<int> DamageTaken;
        public event Action Died;

        private int _livesRemaining = 4;
        private int _diamondsCount = 0;

        public bool IsAlive { get; private set; } = true;
        public int DiamondsCount => _diamondsCount;

        private void Start()
        {
            DiamondsCountUpdated?.Invoke();
        }

        public void ApplyDamage(int damage)
        {
            if (!IsAlive) return;

            _livesRemaining -= damage;
            DamageTaken?.Invoke(_livesRemaining);
            
            if (_livesRemaining <= 0)
            {
                Died?.Invoke();
                IsAlive = false;
            }
            
        }

        public void AddDiamonds(int amount)
        {
            _diamondsCount += amount;
            DiamondsCountUpdated?.Invoke();
        }

        public void RemoveDiamonds(int amount)
        {
            _diamondsCount -= amount;
            DiamondsCountUpdated?.Invoke();
        }
        
    }
}