using System;
using Animations;
using Shop;
using UnityEngine;

namespace Player
{
   [RequireComponent(typeof(InputHandler))]
   [RequireComponent(typeof(Movement))]
   [RequireComponent(typeof(Player))]
   public class AnimationHandler : MonoBehaviour
   {
      [SerializeField] private GameObject _swordArcPrefab;

      private Animator _animator;
      private Animator _swordAnimator;
      private InputHandler _input;
      private Movement _movement;
      private Player _player;

      private const string Move = "Move";
      private const string Jumping = "Jumping";
      private const string Attack = "Attack";
      private const string Death = "Death";

      private void Awake()
      {
         _animator = GetComponentInChildren<Animator>();
         _input = GetComponent<InputHandler>();
         _movement = GetComponent<Movement>();
         _player = GetComponent<Player>();
      }
      
      private void OnEnable()
      {
         _input.OnMovementButtonPressed += SetMoveAnimationParam;
         _input.OnAttackButtonPressed += SetAttackAnimationParam;
         _movement.IsJumping += SetJumpAnimationParam;
         _player.Died += Die;
      }

      private void SetAttackAnimationParam()
      {
         if (ShopDisplayUI.Instance.IsShopEnabled || _animator.GetBool(Jumping)) return;

         _animator.SetTrigger(Attack);
      }

      public void CreateSwordArcEffect(float direction)
      {
         var swordArc = Instantiate(_swordArcPrefab, transform);
         swordArc.transform.localScale = new Vector3(direction, direction);
         
         if (swordArc.TryGetComponent(out SwordArcAnimationHandler handler))
            handler.Init(GetComponentInChildren<SpriteRenderer>());
      }

      private void SetJumpAnimationParam(bool isJumping)
      {
         _animator.SetBool(Jumping, isJumping);
      }
      
      private void SetMoveAnimationParam(float param)
      {
         _animator.SetFloat(Move, Mathf.Abs(param));
      }

      private void Die()
      {
         _animator.SetTrigger(Death);
      }
      
      private void OnDisable()
      {
         _input.OnMovementButtonPressed -= SetMoveAnimationParam;
         _input.OnAttackButtonPressed -= SetAttackAnimationParam;
         _movement.IsJumping -= SetJumpAnimationParam;
         _player.Died -= Die;
      }
   }
}
