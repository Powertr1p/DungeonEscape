using Interfaces;
using UnityEngine;

namespace Enemy
{
   [RequireComponent(typeof(Collider2D))]
   public class SpiderAcid : MonoBehaviour
   {
      private int _damage;
      private float _speed;
   
      public void Init(int damage, float speed)
      {
         _damage = damage;
         _speed = speed;
         
         Destroy(gameObject, 3f);
      }

      private void FixedUpdate()
      {
         transform.Translate(Vector2.right * _speed);
      }

      private void OnTriggerEnter2D(Collider2D other)
      {
         if (other.TryGetComponent(out IDamagable entity))
            entity.ApplyDamage(_damage);
         
         Destroy(gameObject);
      }
   }
}
