using Core;
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
      [SerializeField] private GameObject _bloodPrefab;
      [SerializeField] private GameObject _JumpEffectPrefab;

      private Animator _animator;
      private Animator _swordAnimator;
      private InputHandler _input;
      private Movement _movement;
      private Player _player;

      private string _currentAttack;
      
      private const string FireAttack = "FireAttack";
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
         _player.DamageTaken += InstantiateBlood;
         _player.Died += Die;

         GameEventsHandler.Instance.OnBootsOfLightBought += EnableJumpEffects;
         GameEventsHandler.Instance.OnFlameSwordBought += ChangeAttack;
      }

      private void Start()
      {
         _currentAttack = Attack;
      }

      private void SetAttackAnimationParam()
      {
         if (ShopDisplayUI.Instance.IsShopEnabled || _animator.GetBool(Jumping)) return;

         _animator.SetTrigger(_currentAttack);
      }

      private void EnableJumpEffects()
      {
         _movement.IsJumping += SpawnJumpEffects;
      }

      private void SpawnJumpEffects(bool obj)
      {
         var jumpEffect = Instantiate(_JumpEffectPrefab, transform);
         Destroy(jumpEffect, 1f);
      }

      private void SetJumpAnimationParam(bool isJumping)
      {
         _animator.SetBool(Jumping, isJumping);
      }
      
      private void SetMoveAnimationParam(float param)
      {
         _animator.SetFloat(Move, Mathf.Abs(param));
      }

      private void InstantiateBlood(int damage)
      {
         var direction = GetComponentInChildren<SpriteRenderer>().transform.localScale.x;
         var blood = Instantiate(_bloodPrefab, transform);
         blood.transform.localScale *= direction * -1;
         Destroy(blood, 0.5f);
      }

      private void Die()
      {
         _animator.SetTrigger(Death);
      }

      private void ChangeAttack()
      {
         _currentAttack = FireAttack;
      }
      
      private void OnDisable()
      {
         _input.OnMovementButtonPressed -= SetMoveAnimationParam;
         _input.OnAttackButtonPressed -= SetAttackAnimationParam;
         _movement.IsJumping -= SetJumpAnimationParam;
         _movement.IsJumping -= SpawnJumpEffects;
         _player.Died -= Die;
         
         GameEventsHandler.Instance.OnBootsOfLightBought -= EnableJumpEffects;
         GameEventsHandler.Instance.OnFlameSwordBought -= ChangeAttack;
      }
   }
}
