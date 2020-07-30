using System;
using System.Collections;
using Core;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(Collisions))]
    public class Movement : MonoBehaviour
    {
        public event Action<bool> IsJumping;
        
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _minMovingSpeed = 0.1f;
        
        private Rigidbody2D _rigidbody2D;
        private InputHandler _input;
        private Collisions _collisions;
        private Animator _animator;

        private bool IsPlayerPerformAnAction => (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") ||
                                                _animator.GetCurrentAnimatorStateInfo(0).IsName("FireSwordAttack")) &&
                                                !_animator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        private void Awake()
        {
            _input = GetComponent<InputHandler>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collisions = GetComponent<Collisions>();
            _animator = GetComponentInChildren<Animator>();
        }
        
        private void OnEnable()
        {
            _input.OnMovementButtonPressed += MoveCharacter;
            _input.OnJumpButtonPressed += TryJump;
            
            GameEventsHandler.Instance.OnBootsOfLightBought += EnableBootsOfFlightParams;
        }

        private void Update()
        {
            if (!GameEventsHandler.Instance.IsPlayerAlive)
                OnPlayerDied();
        }
        
        private void MoveCharacter(float direction)
        {
            if (IsPlayerPerformAnAction || Mathf.Abs(direction) <= _minMovingSpeed)
                direction = 0;
                
            _rigidbody2D.velocity = new Vector2(direction * _movementSpeed, _rigidbody2D.velocity.y);
        }
        
        private void TryJump()
        {
            if (!_collisions.IsGrounded()) return;
            StartCoroutine(Jump());
        }

        private IEnumerator Jump()
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,  _jumpForce);
            
            yield return new WaitUntil(() => !_collisions.IsGrounded());
            
            IsJumping?.Invoke(true);
           
            yield return new WaitUntil(() => _collisions.IsGrounded());
            
            IsJumping?.Invoke(false);
        }

        private void OnPlayerDied()
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
        
        private void EnableBootsOfFlightParams()
        {
            DoubledJumpForce();
        }

        private void DoubledJumpForce()
        {
            _jumpForce *= 2f;
        }
        
        private void OnDisable()
        {
            _input.OnMovementButtonPressed -= MoveCharacter;
            _input.OnJumpButtonPressed -= TryJump;
            
            GameEventsHandler.Instance.OnBootsOfLightBought -= EnableBootsOfFlightParams;
        }
    }
}
