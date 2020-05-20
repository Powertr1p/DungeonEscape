using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FlipSprite : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void OnEnable()
        {
            GetComponentInParent<InputHandler>().OnMovementButtonPressed += Flip;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void Flip (float facingDirection)
        {
            if (Mathf.Abs(facingDirection) < 1) return;
            
            _spriteRenderer.flipX = Mathf.Abs(facingDirection - 1) > 1;
        }
    }
}
