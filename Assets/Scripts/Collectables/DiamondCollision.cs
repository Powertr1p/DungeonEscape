using System;
using UnityEngine;

namespace Collectables
{
    [RequireComponent(typeof(Collider2D))]
    public class DiamondCollision : MonoBehaviour
    {
        private Rigidbody2D _rb2d;

        private void Awake()
        {
            _rb2d = GetComponentInParent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _rb2d.velocity = new Vector2(0, _rb2d.velocity.y);
        }
    }
}