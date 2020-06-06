﻿using System;
using UnityEngine;

namespace Enemy
{
    public class Spider : Enemy
    {
        [SerializeField] private GameObject _acidPrefab;

        [SerializeField] private float _projectileSpeed = 0.15f;
        [SerializeField] private Transform _projectileSpawnPivot;
        
        private SpiderAnimationEvent _spiderEvent;

        protected override void Init()
        {
            base.Init();
            
            _spiderEvent = GetComponentInChildren<SpiderAnimationEvent>();
            _spiderEvent.OnFire += Attack;
        }

        protected override void Update()
        {
        }

        private void Attack()
        {
            var projectile = Instantiate(_acidPrefab, _projectileSpawnPivot.position, Quaternion.identity);
            projectile.GetComponent<SpiderAcid>().Init(GetDamageValue(), _projectileSpeed);
        }

        protected override void Move()
        {
        }

        private void OnDisable()
        {
            _spiderEvent.OnFire -= Attack;
        }
    }
}


