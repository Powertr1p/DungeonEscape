using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IDamagable
    {
        public event Action DiamondsCountUpdated;
        public event Action<int> DamageTaken;

        private int _livesRemaining = 4;
        private int _diamondsCount = 0;
        public int DiamondsCount => _diamondsCount;

        private void Start()
        {
            DiamondsCountUpdated?.Invoke();
        }

        public void ApplyDamage(int damage)
        {
            if (_livesRemaining > 0)
            {
                _livesRemaining -= damage;
                DamageTaken?.Invoke(_livesRemaining);   
            }
            else
            {
                //play death   
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