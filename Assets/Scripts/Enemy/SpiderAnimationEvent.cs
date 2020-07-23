using System;
using UnityEngine;

namespace Enemy
{
    public class SpiderAnimationEvent : MonoBehaviour
    {
        public event Action OnFire;
        
        public void Fire() //using from animation event
        {
            OnFire?.Invoke();
        }
    }
}
