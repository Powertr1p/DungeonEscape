using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IDamagable
    {
        public event Action DiamondsCountUpdated;

        private int _diamondsCount = 0;
        public int DiamondsCount => _diamondsCount;

        private void Start()
        {
            DiamondsCountUpdated?.Invoke();
        }

        public void ApplyDamage(int damage)
        {
            Debug.Log("Player was attacked!");
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