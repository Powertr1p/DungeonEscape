using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float _movementSpeed = 10f;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        
        _rigidbody2D.velocity = new Vector2(horizontalInput * _movementSpeed, _rigidbody2D.velocity.y);
    }
}
