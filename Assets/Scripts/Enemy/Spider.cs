using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class Spider : Enemy
    {
        [SerializeField] private GameObject _acidPrefab;

        [SerializeField] private float _projectileSpeed = 0.15f;
        [SerializeField] private Transform _projectileSpawnPivot;
        [SerializeField] private Transform _bloodSpawnPivot;

        private SpiderAnimationEvent _spiderEvent;

        protected override void Init()
        {
            base.Init();
            
            _spiderEvent = GetComponentInChildren<SpiderAnimationEvent>();
            _spiderEvent.OnFire += Attack;
        }

        protected override void Update()
        {
            if (IsDead) return;

            TryToggleCombat(IsPlayerSpotted());
            
            if (IsPlayerSpotted() && !Animator.GetBool(InCombat))
                Animator.SetBool(InCombat, true);
            else if (!IsPlayerSpotted() && Animator.GetBool(InCombat))
                Animator.SetBool(InCombat, false);
        }

        public override void ApplyDamage(int damage)
        {
            base.ApplyDamage(damage);
            StartCoroutine(ChangeColorOnHit());
        }

        protected override void InstantiateDamageEffect()
        {
            var blood = Instantiate(HitEffectPrefab, _bloodSpawnPivot.position, Quaternion.identity);
            Destroy(blood, 0.5f);
        }

        private IEnumerator ChangeColorOnHit()
        {
            Sprite.color = new Color(1,0,0, 1);
            yield return new WaitForSeconds(0.2f);
            Sprite.color = new Color(1,1,1, 1);
        }

        private void Attack()
        {
            var projectile = Instantiate(_acidPrefab, _projectileSpawnPivot.position, Quaternion.identity);
            projectile.GetComponent<SpiderAcid>().Init(GetDamageValue(), _projectileSpeed);
        }

        protected override void Move(Vector2 position)
        {
        }

        protected override void SetupWaypointsAndTarget()
        {
        }

        private void OnDisable()
        {
            _spiderEvent.OnFire -= Attack;
        }
    }
}


