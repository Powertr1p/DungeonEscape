using System;
using UnityEngine;

namespace Enemy
{
    public class Spider : Enemy
    {
        [SerializeField] private GameObject _acidPrefab;

        [SerializeField] private float _projectileSpeed = 0;
        
        private SpiderAnimationEvent _spiderEvent;

        protected override void Init()
        {
            base.Init();
            
            _spiderEvent = GetComponentInChildren<SpiderAnimationEvent>();
            _spiderEvent.OnFire += Attack;
        }

        private void Attack()
        {
            var projectile = Instantiate(_acidPrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<SpiderAcid>().Init(GetDamageValue(), _projectileSpeed);
        }

        protected override void Move()
        {
            
        }
    }
}


