using System;
using System.Collections;
using Collectables;
using Core;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace Enemy
{
    [RequireComponent(typeof(DamageDealer))]
    public abstract class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] protected int Health;
        [SerializeField] protected float Speed;
        [SerializeField] protected int Diamonds = 1;
        [SerializeField] protected Transform WaypointA;
        [SerializeField] protected Transform WaypointB;
        [SerializeField] protected Transform Player;
        [SerializeField] protected GameObject DiamondPrefab;
        [SerializeField] protected float SpotingRayDistance = 2f;
        [SerializeField] protected GameObject HitEffectPrefab;
        [SerializeField] protected Transform HitEffectSpawnPivot;

        private bool _isPlayerAlive => GameEventsHandler.Instance.IsPlayerAlive;
        
        protected Transform Target;
        protected Animator Animator;
        protected SpriteRenderer Sprite;
        protected DamageDealer Damage;
        
        protected bool IsDead = false;
        
        protected const string Idle = "Idle";
        protected const string Hit = "Hit";
        protected const string InCombat = "InCombat";
        protected const string Death = "Death";

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            Animator = GetComponentInChildren<Animator>();
            Sprite = GetComponentInChildren<SpriteRenderer>();
            Damage = GetComponent<DamageDealer>();
            
            SetupWaypointsAndTarget();
        }

        protected virtual void SetupWaypointsAndTarget()
        {
            WaypointA.position = new Vector2(WaypointA.position.x, transform.position.y);
            WaypointB.position = new Vector2(WaypointB.position.x, transform.position.y);

            Target = WaypointB.transform;
        }

        protected virtual void Update()
        {
            if (IsDead) return;
            
            if (Animator.GetCurrentAnimatorStateInfo(0).IsName(Idle)) return;

            FlipWhileWalking();
            TryChangeMoveDirection();

            if (Animator.GetBool(InCombat))
                FaceToPlayerWhenAttack();
            else
                Move(Target.position);
            
            TryToggleCombat(IsPlayerSpotted());
        }
        
        protected int GetDamageValue() => Damage.GetDamageValue;

        protected virtual void TryToggleCombat(bool isPlayerSpotted)
        {
            if (isPlayerSpotted && !Animator.GetBool(InCombat) && Vector3.Distance(transform.localPosition, Player.localPosition) < 0.5f)
                ToggleCombatMode(true);
            else if (Vector3.Distance(transform.localPosition, Player.localPosition) > 1.5f || !_isPlayerAlive)
                ToggleCombatMode(false);
        }

        private void FlipWhileWalking()
        {
            if (Target.position == WaypointA.position)
                FlipSprite(true);
            else if (Target.position == WaypointB.position)
                FlipSprite(false);
        }

        private void FlipSprite(bool isFlip)
        {
            var currentDirection = transform.localScale;

            if (isFlip && currentDirection.x < 0 || !isFlip && currentDirection.x > 0) return;
            
            var flipped = new Vector2(-currentDirection.x, currentDirection.y);
            transform.localScale = flipped;
        }

        private void FaceToPlayerWhenAttack()
        {
            var direction = Player.localPosition - transform.position;

            FlipSprite(!(direction.x > 0));
        }

        private void TryChangeMoveDirection()
        {
            if (transform.position == WaypointA.position)
            {
                Target = WaypointB.transform;
                ToggleIdleAnimation();
            }
            else if (transform.position == WaypointB.position)
            {
                Target = WaypointA.transform;
                ToggleIdleAnimation();
            }
        }
        
        private void ToggleIdleAnimation()
        {
            Animator.SetTrigger(Idle);
        }

        protected virtual void Move(Vector2 position)
        {
           if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
            
            var target = new Vector2(position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        }
        
        protected virtual void ToggleCombatMode(bool isCombat)
        {
            Animator.SetBool(InCombat, isCombat);
        }
        
        public virtual void ApplyDamage(int damage)
        {
            if (IsDead) return;

            if (Health > 0)
                Health -= damage;
            
            InstantiateDamageEffect();
            
            if (!Animator.GetBool(InCombat))
                Animator.SetTrigger(Hit);
            
            ToggleCombatMode(true);

            if (Health <= 0)
                Die();
        }
        
        protected virtual void InstantiateDamageEffect()
        {
            var direction = transform.localScale.x;
            var effect = Instantiate(HitEffectPrefab, HitEffectSpawnPivot);
            effect.transform.localScale *= direction * -1;
            Destroy(effect, 0.5f);
        }

        protected virtual bool IsPlayerSpotted()
        {
            if (!_isPlayerAlive) return false;
            
            Debug.DrawRay(transform.position, new Vector3(transform.localScale.x * SpotingRayDistance, 0,0));
            
            var hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), SpotingRayDistance, LayerMask.GetMask("PlayerHitbox"));

            return !ReferenceEquals(hit.collider, null);
        }

        private void Die()
        {
            Animator.SetTrigger(Death);

            SpawnDiamonds();

            IsDead = true;
        }

        protected virtual void SpawnDiamonds()
        {
            for (int i = 0; i < Diamonds; i++)
            {
                var diamond = Instantiate(DiamondPrefab, transform.position, Quaternion.identity).GetComponent<Diamond>();
                diamond.SpawnFromEnemy();
            }
        }
    }
}
