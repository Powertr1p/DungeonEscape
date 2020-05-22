using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(InputHandler))]
    public class Movement : MonoBehaviour
    {
        public event Action<bool> IsJumping;
        
        private Rigidbody2D _rigidbody2D;
        private InputHandler _input;

        [SerializeField] LayerMask _groundLayer;
        
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _jumpForce = 5f;

        private float _rayDistance = 0.6f;
        private bool _isGrounded;
        
        private void Awake()
        {
            _input = GetComponent<InputHandler>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
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

        private void FixedUpdate()
        {
            Debug.DrawRay(transform.position, Vector3.down * _rayDistance, Color.green);
            _isGrounded = IsGrounded();
        }

        private void TryJump()
        {
            if (!IsGrounded()) return;
            StartCoroutine(Jump());
        }

        private IEnumerator Jump()
        {
            IsJumping?.Invoke(true);
            yield return new WaitForSeconds(0.2f);
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,  _jumpForce);
            yield return  new WaitForSeconds(1f);
            IsJumping?.Invoke(false);
        }
        
        private bool IsGrounded()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, _groundLayer.value);
            return hit.collider != null;
        }

        private void OnDisable()
        {
            _input.OnMovementButtonPressed -= MoveCharacter;
            _input.OnJumpButtonPressed -= TryJump;
        }
    }
}
