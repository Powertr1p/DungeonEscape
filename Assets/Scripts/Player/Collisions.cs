using System;
using UnityEngine;

namespace Player
{
    public class Collisions : MonoBehaviour
    {
        [SerializeField] private FlipSprite _spriteFlipper;
        
        [SerializeField] private Vector3 _collisionRayOffset;
        [SerializeField] private float _rayDistance = 0.8f;
        [SerializeField] LayerMask _groundLayer;

        private bool _isSpriteFlipped => _spriteFlipper.IsSpriteFlipped;
        
        private void FixedUpdate() //for debugging
        {
            TryFlipRay();
            Debug.DrawRay(transform.position + _collisionRayOffset, Vector3.down * _rayDistance, Color.green);
        }

        private void TryFlipRay()
        {
            var isRayFlipped = _collisionRayOffset.x > 0;
            
            if (_isSpriteFlipped != isRayFlipped)
                _collisionRayOffset = new Vector3(_collisionRayOffset.x * -1, _collisionRayOffset.y, _collisionRayOffset.z);
        }
        
        
        public bool IsGrounded()
        {
            TryFlipRay();
            
            var hit = Physics2D.Raycast(transform.position + _collisionRayOffset, Vector2.down, _rayDistance, _groundLayer.value);
            return hit.collider != null;
        }
    }
}
