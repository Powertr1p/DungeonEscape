using Core;
using Interfaces;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(DamageDealer))]
    public abstract class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] protected int Health;
        [SerializeField] protected float Speed;
        [SerializeField] protected int Gems;
        [SerializeField] protected Transform WaypointA;
        [SerializeField] protected Transform WaypointB;
        [SerializeField] protected Transform Player;

        protected Transform Target;
        protected Animator Animator;
        protected SpriteRenderer Sprite;
        protected DamageDealer Damage;

        protected bool IsHitted = false;
        protected bool IsDead = false;
        
        private const string Idle = "Idle";
        private const string Hit = "Hit";
        private const string InCombat = "InCombat";
        private const string Death = "Death";

        private void Start()
        {
            Init();

            WaypointA.position = new Vector2(WaypointA.position.x, transform.position.y);
            WaypointB.position = new Vector2(WaypointB.position.x, transform.position.y);

            Target = WaypointB.transform;
        }

        protected virtual void Init()
        {
            Animator = GetComponentInChildren<Animator>();
            Sprite = GetComponentInChildren<SpriteRenderer>();
            Damage = GetComponent<DamageDealer>();
        }

        protected virtual void Update()
        {
            if (IsDead) return;
            
            TryExitCombatMode();
            
            if (Animator.GetCurrentAnimatorStateInfo(0).IsName(Idle)) return;

            FlipWhileWalking();
            TryChangeMoveDirection();
            
            if (!IsHitted)
                Move();

            if (Animator.GetBool(InCombat) == true)
                FaceToPlayerWhenAttack();
        }
        
        protected int GetDamageValue() => Damage.GetDamageValue;

        protected virtual void TryExitCombatMode()
        {
            if (Vector3.Distance(this.transform.localPosition, Player.localPosition) < 2f) return;
            
            ToggleCombatMode(false);
        }

        private void FlipWhileWalking()
        {
            if (Target.position == WaypointA.position)
                Sprite.flipX = true;
            else
                Sprite.flipX = false;
        }

        private void FaceToPlayerWhenAttack()
        {
            var direction = Player.localPosition - transform.position;

            Sprite.flipX = !(direction.x > 0);
        }

        private void TryChangeMoveDirection()
        {
            if (transform.position == WaypointA.position)
            {
                Target = WaypointB.transform;
                ToggleIdleAnimation();
            }
            else if (transform.position == WaypointB.position)
            {
                Target = WaypointA.transform;
                ToggleIdleAnimation();
            }
        }
        
        private void ToggleIdleAnimation()
        {
            Animator.SetTrigger(Idle);
        }

        protected virtual void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
        
        protected virtual void ToggleCombatMode(bool isCombat)
        {
            IsHitted = isCombat;
            Animator.SetBool(InCombat, isCombat);
        }
        
        public void ApplyDamage(int damage)
        {
            if (Health > 0)
                Health -= damage;
            
            Animator.SetTrigger(Hit);
            ToggleCombatMode(true);

            if (Health <= 0)
                Die();
        }
        
        private void Die()
        {
            Animator.SetTrigger(Death);
            IsDead = true;
        }
    }
}
