using System;
using UnityEditor;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(InputHandler))]
    public class Movement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        [SerializeField] LayerMask _groundLayer;
        
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _jumpForce = 5f;

        private float _rayDistance = 0.6f;
        

        private void OnEnable()
        {
            GetComponent<InputHandler>().OnMovementButtonPressed += MoveCharacter;
            GetComponent<InputHandler>().OnJumpButtonPressed += TryJump;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void MoveCharacter(float direction)
        {
            _rigidbody2D.velocity = new Vector2(direction * _movementSpeed, _rigidbody2D.velocity.y);
        }

        private void TryJump()
        {
            if (!IsGrounded()) return;
            
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,  _jumpForce);
        }
        
        private bool IsGrounded()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, _groundLayer.value);
            return hit.collider != null;
        }

        private void OnDisable()
        {
            GetComponent<InputHandler>().OnMovementButtonPressed -= MoveCharacter;
            GetComponent<InputHandler>().OnJumpButtonPressed -= TryJump;
        }
        
    }
}
