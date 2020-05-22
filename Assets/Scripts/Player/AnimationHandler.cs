using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
   public class AnimationHandler : MonoBehaviour
   {
      private Animator _animator;
      private InputHandler _input;
      private Movement _movement;

      private const string Move = "Move";
      private const string Jumping = "Jumping";
      
      private void Awake()
      {
         _animator = GetComponentInChildren<Animator>();
         _input = GetComponent<InputHandler>();
         _movement = GetComponent<Movement>();
      }
      
      private void OnEnable()
      {
         _input.OnMovementButtonPressed += SetMoveAnimationParam;
         _movement.IsJumping += SetJumpAnimationParam;
      }
      
      public void SetJumpAnimationParam(bool isJumping)
      {
         _animator.SetBool(Jumping, isJumping);
      }
      
      private void SetMoveAnimationParam(float param)
      {
         _animator.SetFloat(Move, Mathf.Abs(param));
      }
      
      private void OnDisable()
      {
         _input.OnMovementButtonPressed -= SetMoveAnimationParam;
         _movement.IsJumping -= SetJumpAnimationParam;
      }
   }
}
