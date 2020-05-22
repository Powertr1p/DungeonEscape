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

      private const string Move = "Move";
      
      private void Awake()
      {
         _animator = GetComponentInChildren<Animator>();
         _input = GetComponent<InputHandler>();
      }
      
      private void OnEnable()
      {
         _input.OnMovementButtonPressed += SetMoveAnimationParam;
      }
      
      private void SetMoveAnimationParam(float param)
      {
         Debug.Log(param);
         _animator.SetFloat(Move, Mathf.Abs(param));
      }

      private void OnDisable()
      {
         _input.OnMovementButtonPressed -= SetMoveAnimationParam;
      }
   }
}
