using System;
using UnityEngine;

namespace Enemy
{
    public class SpiderAnimationEvent : MonoBehaviour
    {
        public event Action OnFire;
    
        //using through event animation
        public void Fire()
        {
            OnFire?.Invoke();
        }
    }
}
