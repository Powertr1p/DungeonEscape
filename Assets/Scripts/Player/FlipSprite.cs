using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FlipSprite : MonoBehaviour
    {
        private InputHandler _input;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _input = GetComponentInParent<InputHandler>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void OnEnable()
        {
            _input.OnMovementButtonPressed += Flip;
        }
        
        private void Flip (float facingDirection)
        {
            if (Mathf.Abs(facingDirection) < 0) return;

            _spriteRenderer.flipX = facingDirection < 0;
        }

        private void OnDisable()
        {
            _input.OnMovementButtonPressed -= Flip;
        }
    }
}
