using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float _movementSpeed = 5f;

    private void OnEnable()
    {
        GetComponent<PlayerInput>().OnMovementButtonPressed += MoveCharacter;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
    }

    private void MoveCharacter(float direction)
    {
        _rigidbody2D.velocity = new Vector2(direction * _movementSpeed, _rigidbody2D.velocity.y);
    }
}
