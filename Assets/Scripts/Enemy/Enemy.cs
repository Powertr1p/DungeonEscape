using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected int Health;
        [SerializeField] protected float Speed;
        [SerializeField] protected int Gems;
        [SerializeField] protected Transform WaypointA;
        [SerializeField] protected Transform WaypointB;

        protected Transform Target;
        protected Animator Animator;
        protected SpriteRenderer Sprite;

        private const string Idle = "Idle";

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
            if (Animator.GetCurrentAnimatorStateInfo(0).IsName(Idle)) return;

            Flip();
            TryChangeMoveDirection();
            Move();
        }

        private void Flip()
        {
            if (Target.position == WaypointA.position)
                Sprite.flipX = true;
            else
                Sprite.flipX = false;
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
    }
}
