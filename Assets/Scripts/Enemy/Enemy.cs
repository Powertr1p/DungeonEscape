﻿using Interfaces;
using UnityEngine;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] protected int Health;
        [SerializeField] protected float Speed;
        [SerializeField] protected int Gems;
        [SerializeField] protected Transform WaypointA;
        [SerializeField] protected Transform WaypointB;
        [SerializeField] protected Transform Player;

        protected Transform Target;
        protected Animator Animator;
        protected SpriteRenderer Sprite;

        protected bool IsHitted = false;
        
        private const string Idle = "Idle";
        private const string Hit = "Hit";
        private const string InCombat = "InCombat";

        private void Start()
        {
            Init();

            WaypointA.position = new Vector2(WaypointA.position.x, transform.position.y);
            WaypointB.position = new Vector2(WaypointB.position.x, transform.position.y);

            Target = WaypointB.transform;
        }

        public virtual void Init()
        {
            Animator = GetComponentInChildren<Animator>();
            Sprite = GetComponentInChildren<SpriteRenderer>();
        }

        protected virtual void Update()
        {
            CheckDistanceBetweenPlayer();
            
            if (Animator.GetCurrentAnimatorStateInfo(0).IsName(Idle)) return;

            FlipWhileWalking();
            TryChangeMoveDirection();
            
            if (!IsHitted)
                Move();

            if (Animator.GetBool(InCombat))
                FaceToPlayerWhenAttack();
        }

        private void CheckDistanceBetweenPlayer()
        {
            if (!(Vector3.Distance(this.transform.localPosition, Player.localPosition) > 2f)) return;
            
            IsHitted = false;
            Animator.SetBool(InCombat, false);
        }

        private void FlipWhileWalking()
        {
            if (Target.position == WaypointA.position)
                Sprite.flipX = true;
            else
                Sprite.flipX = false;
        }

        private void FaceToPlayerWhenAttack()
        {
            var direction = Player.localPosition - transform.position;

            Sprite.flipX = !(direction.x > 0);
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

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }

        private void ToggleIdleAnimation()
        {
            Animator.SetTrigger(Idle);
        }

        public void ApplyDamage(int damage)
        {
            if (Health > 0)
                Health -= damage;
            
            Animator.SetTrigger(Hit);
            IsHitted = true;
            Animator.SetBool(InCombat, true);

            if (Health <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
