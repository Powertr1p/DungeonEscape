using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(Collisions))]
    public class Movement : MonoBehaviour
    {
        public event Action<bool> IsJumping;
        
        private Rigidbody2D _rigidbody2D;
        private InputHandler _input;
        private Collisions _collisions;

        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _jumpForce = 7f;

        private void Awake()
        {
            _input = GetComponent<InputHandler>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collisions = GetComponent<Collisions>();
        }
        
        private void OnEnable()
        {
            _input.OnMovementButtonPressed += MoveCharacter;
            _input.OnJumpButtonPressed += TryJump;
        }
        
        private void MoveCharacter(float direction)
        {
            _rigidbody2D.velocity = new Vector2(direction * _movementSpeed, _rigidbody2D.velocity.y);
        }

        private void TryJump()
        {
            if (!_collisions.IsGrounded()) return;
            StartCoroutine(Jump());
        }

        private IEnumerator Jump()
        {
            IsJumping?.Invoke(true);
            
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,  _jumpForce);
            
            yield return new WaitUntil(() => !_collisions.IsGrounded());
            yield return new WaitUntil(() => _collisions.IsGrounded());
            
            IsJumping?.Invoke(false);
        }

        private void OnDisable()
        {
            _input.OnMovementButtonPressed -= MoveCharacter;
            _input.OnJumpButtonPressed -= TryJump;
        }
    }
}
