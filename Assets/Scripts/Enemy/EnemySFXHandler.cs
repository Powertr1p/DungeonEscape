using System;
using UnityEngine;

namespace Enemy
{
    public class EnemySFXHandler : MonoBehaviour
    {
        private Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponentInParent<Enemy>();
        }

        public void PlayFootsteps() //using from animation event
        {
            _enemy.PlayFootstepsSound();
        }

        public void PlayAttack() //using from animation event
        {
            _enemy.PlayAttackSound();
        }
    }
}