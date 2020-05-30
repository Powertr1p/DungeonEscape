using System;
using UnityEngine;

namespace Enemy
{
    public class Spider : Enemy
    {
        private Transform _target;
        private Animator _animator;
        private SpriteRenderer _sprite;

        private const string Idle = "Idle";

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            WaypointA.position = new Vector2(WaypointA.position.x, transform.position.y);
            WaypointB.position = new Vector2(WaypointB.position.x, transform.position.y);
            
            _target = WaypointB.transform;
        }

        protected override void Update()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(Idle)) return;
        
            Flip();
            TryChangeMoveDirection();
            Move();
        }

        private void TryChangeMoveDirection()
        { 
            if (transform.position == WaypointA.position) 
            { 
                _target = WaypointB.transform; 
                ToggleIdleAnimation();
            }
            
            else if (transform.position == WaypointB.position)
            {
                _target = WaypointA.transform;
                ToggleIdleAnimation();
            }
        }

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, Speed * Time.deltaTime);
        }

        private void ToggleIdleAnimation()
        {
            _animator.SetTrigger(Idle);
        }

        private void Flip()
        { 
            if (_target.position == WaypointA.position)
                _sprite.flipX = true;
            else
                _sprite.flipX = false;
        }
            
    }
}


