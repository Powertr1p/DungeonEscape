using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SpiderAcid : MonoBehaviour
{
   private int _damage = 0;
   private int _speed = 0;
   
   public void Init(int damage, int speed)
   {
      _damage = damage;
      _speed = speed;
   }

   private void Update()
   {
      transform.Translate(Vector2.right * _speed * Time.deltaTime);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.TryGetComponent(out IDamagable entity))
         entity.ApplyDamage(_damage);
   }
}
