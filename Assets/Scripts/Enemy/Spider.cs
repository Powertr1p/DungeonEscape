using System;
using UnityEngine;

namespace Enemy
{
    public class Spider : Enemy
    {
        private SpiderAnimationEvent _spiderEvent;

        public override void Init()
        {
            base.Init();
            
            _spiderEvent = GetComponentInChildren<SpiderAnimationEvent>();
            _spiderEvent.OnFire += Attack;
        }

        public void Attack()
        {
            
        }
    }
}


