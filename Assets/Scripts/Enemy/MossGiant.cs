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

    private const string Idle = "Idle";

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _target = _waypointB.transform;
    }
    
    
    protected override void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(Idle)) return;
        
       
        TryChangeMoveDirection();
        Move();
    }

    private void TryChangeMoveDirection()
    {
        if (transform.position.x == _waypointA.position.x)
        {
            _target = _waypointB.transform;
            ToggleIdleAnimation();
        }
        else if (transform.position.x == _waypointB.position.x)
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
    
}
