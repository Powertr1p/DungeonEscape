using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Enemy
{ 
    public abstract class Enemy : MonoBehaviour 
    { 
        [SerializeField] protected int Health;
        [SerializeField] protected float Speed;
        [SerializeField] protected int Gems;
        [SerializeField] protected Transform WaypointA;
        [SerializeField] protected Transform WaypointB;
        
        protected virtual void Attack()
        {
        
        }
        
        protected abstract void Update();
}
}
