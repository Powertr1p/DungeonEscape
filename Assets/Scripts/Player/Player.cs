using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Player : MonoBehaviour, IDamagable
    {
        private Collider2D _collider2D;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        public event Action<int> DamageTaken;
        public event Action Died;

        private int _livesRemaining = 4;

        public bool IsAlive => _livesRemaining > 0;
        public int DiamondsCount { get; set; }

        public void ApplyDamage(int damage)
        {
            if (!IsAlive) return;

            _livesRemaining -= damage;
            DamageTaken?.Invoke(_livesRemaining);

            if (_livesRemaining <= 0)
            {
                Died?.Invoke();
                _rigidbody2D.gravityScale = 0;
                _collider2D.enabled = false;
            }
        }
    }
}