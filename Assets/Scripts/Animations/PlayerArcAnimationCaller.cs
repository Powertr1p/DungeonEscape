using System;
using Player;
using UnityEngine;

namespace Animations
{
    public class PlayerArcAnimationCaller : MonoBehaviour
    {
        private AnimationHandler _animHandler;

        private void Awake()
        {
            _animHandler = GetComponentInParent<AnimationHandler>();
        }

        //calling from animation through event
        private void StartArcAnimation()
        {
            _animHandler.CreateSwordArcEffect();
        }
    }
}
