using System;
using System.Collections;
using System.Collections.Generic;
using Animations;
using UnityEngine;

namespace Player
{
   public class AnimationHandler : MonoBehaviour
   {
      [SerializeField] private GameObject _swordArcPrefab;
      
      private Animator _animator;
      private Animator _swordAnimator;
      private InputHandler _input;
      private Movement _movement;

      private const string Move = "Move";
      private const string Jumping = "Jumping";
      private const string Attack = "Attack";

      private void Awake()
      {
         _animator = GetComponentInChildren<Animator>();
         _input = GetComponent<InputHandler>();
         _movement = GetComponent<Movement>();
      }
      
      private void OnEnable()
      {
         _input.OnMovementButtonPressed += SetMoveAnimationParam;
         _input.OnAttackButtonPressed += SetAttackAnimationParam;
         _movement.IsJumping += SetJumpAnimationParam;
      }

      private void SetAttackAnimationParam()
      {
         _animator.SetTrigger(Attack);

         CreateSwordArcEffect();
      }

      private void CreateSwordArcEffect() //сделать паблик и пускай анимация удара вызывает этот метод
      {
         var swordArc = Instantiate(_swordArcPrefab, transform);
         if (swordArc.TryGetComponent(out SwordArcAnimationHandler handler))
            handler.Init(GetComponentInChildren<SpriteRenderer>());
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
         _input.OnAttackButtonPressed -= SetAttackAnimationParam;
         _movement.IsJumping -= SetJumpAnimationParam;
      }
   }
}
