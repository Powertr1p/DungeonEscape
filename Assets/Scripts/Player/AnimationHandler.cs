using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
   public class AnimationHandler : MonoBehaviour
   {
      private Animator _animator;
      private const string Move = "Move";

      private void OnEnable()
      {
         GetComponent<InputHandler>().OnMovementButtonPressed += SetMoveAnimationParam;
      }

      private void Awake()
      {
         _animator = GetComponent<Animator>();
      }

      private void SetMoveAnimationParam(float param)
      {
         _animator.SetFloat(Move, param);
      }
   
   }
}
