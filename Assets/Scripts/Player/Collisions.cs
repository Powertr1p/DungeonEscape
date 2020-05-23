using UnityEngine;

namespace Player
{
    public class Collisions : MonoBehaviour
    {
        [SerializeField] private float _rayDistance = 0.6f;
        [SerializeField] LayerMask _groundLayer;
        
        private void FixedUpdate()
        {
            Debug.DrawRay(transform.position, Vector3.down * _rayDistance, Color.green);
        }
        public bool IsGrounded()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, _groundLayer.value);
            return hit.collider != null;
        }
    }
}
