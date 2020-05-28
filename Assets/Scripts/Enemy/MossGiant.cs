using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    [SerializeField] private Transform _waypointA;
    [SerializeField] private Transform _waypointB;

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
        _waypointA.position = new Vector2(_waypointA.position.x, transform.position.y);
        _waypointB.position = new Vector2(_waypointB.position.x, transform.position.y);
        
        _target = _waypointB.transform;
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
        if (transform.position == _waypointA.position)
        {
            _target = _waypointB.transform;
            ToggleIdleAnimation();
        }
        else if (transform.position == _waypointB.position)
        {
            _target = _waypointA.transform;
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
        if (_target.position == _waypointA.position)
            _sprite.flipX = true;
        else
            _sprite.flipX = false;
    }
}
