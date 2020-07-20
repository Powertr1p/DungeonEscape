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
            transform.position = _playerSprite.transform.position;

            if (_spriteRenderer.flipX)
            {
                var currentPosition = _playerSprite.transform.position;
                transform.position = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z);
                _spriteRenderer.flipY = true;
            }
        }
    }
}
