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
         _animator = GetComponentInChildren<Animator>();
      }

      private void SetMoveAnimationParam(float param)
      {
         Debug.Log(param);
         _animator.SetFloat(Move, Mathf.Abs(param));
      }

      private void TryFlipSprite(float direction)
      {
         
      }
   
   }
}
