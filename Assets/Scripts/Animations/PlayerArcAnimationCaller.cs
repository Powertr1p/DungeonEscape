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

        //calling from animation through animation event (Attack animation on Player/Sprite)
        private void StartArcAnimation()
        {
            _animHandler.CreateSwordArcEffect(transform.localScale.x);
        }
    }
}
