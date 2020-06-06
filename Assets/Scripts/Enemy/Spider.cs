using System;
using UnityEngine;

namespace Enemy
{
    public class Spider : Enemy
    {
        private SpiderAnimationEvent _spiderEvent;

        protected override void Init()
        {
            base.Init();
            
            _spiderEvent = GetComponentInChildren<SpiderAnimationEvent>();
            _spiderEvent.OnFire += Attack;
        }

        private void Attack()
        {
            //instantiate acid
        }

        protected override void Move()
        {
            
        }
    }
}


