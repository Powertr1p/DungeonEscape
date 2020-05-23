using System;
using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SwordArcAnimationHandler : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private SpriteRenderer _playerSprite;

        private float _animationTime = 0.4f;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(SpriteRenderer playerSprite)
        {
            _playerSprite = playerSprite;
        }

        private void Start()
        {
            Destroy(gameObject, _animationTime);
        }

        private void Update()
        {
            _spriteRenderer.flipX = _playerSprite.flipX;
            
            TryFlipSprite();
        }

        private void TryFlipSprite()
        {
            if (_playerSprite.flipX)
            {
                //_playerSprite //давать сюда минус по иксу позицию
            }
        }
    }
}
