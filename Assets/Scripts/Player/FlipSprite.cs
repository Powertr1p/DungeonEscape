using System;
using Core;
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

        public bool IsSpriteFlipped => _spriteRenderer.transform.localScale.x < 0;
        
        private void Flip (float facingDirection)
        {
             if (facingDirection ==  0) return;

             _spriteRenderer.transform.localScale = facingDirection < 0 ? new Vector2(-1, 1) : new Vector2(1, 1);
        }

        private void OnDisable()
        {
            _input.OnMovementButtonPressed -= Flip;
        }
    }
}
