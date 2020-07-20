using System.Collections;
using Collectables;
using Core;
using Interfaces;
using TMPro;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(DamageDealer))]
    public abstract class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] protected int Health;
        [SerializeField] protected float Speed;
        [SerializeField] protected float FollowPlayertSpeedMultiplier = 1.5f;
        [SerializeField] protected int Diamonds = 1;
        
        [SerializeField] protected Transform WaypointA;
        [SerializeField] protected Transform WaypointB;
        [SerializeField] protected Transform Player;
        [SerializeField] protected GameObject DiamondPrefab;
        [SerializeField] protected GameObject HitEffectPrefab;
        [SerializeField] protected GameObject DamageText;
        [SerializeField] protected Transform HitEffectSpawnPivot;
        
        [SerializeField] protected float SpotingRayDistance = 2f;
        [SerializeField] protected float DistanceToToggleOnCombat = 0.5f;
        [SerializeField] protected float DistanceToToggleOffCombat = 1.5f;
        
        private bool _isPlayerAlive => GameEventsHandler.Instance.IsPlayerAlive;
        private Vector2 _position => transform.position;
        
        protected Transform Target;
        protected Animator Animator;
        protected SpriteRenderer Sprite;
        protected DamageDealer Damage;
        
        protected bool IsDead = false;
        private bool _followPlayer;

        protected const string Idle = "Idle";
        protected const string Hit = "Hit";
        protected const string InCombat = "InCombat";
        protected const string Death = "Death";

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            Animator = GetComponentInChildren<Animator>();
            Sprite = GetComponentInChildren<SpriteRenderer>();
            Damage = GetComponent<DamageDealer>();
            
            SetupWaypointsAndTarget();
        }

        protected virtual void SetupWaypointsAndTarget()
        {
            WaypointA.position = new Vector2(WaypointA.position.x, transform.position.y);
            WaypointB.position = new Vector2(WaypointB.position.x, transform.position.y);

            Target = WaypointB.transform;
        }

        protected virtual void Update()
        {
            if (IsDead) return;
            
            if (Animator.GetCurrentAnimatorStateInfo(0).IsName(Idle)) return;

            FlipWhileWalking();
            TryChangeMoveDirection();

            if (Animator.GetBool(InCombat))
                FaceToPlayer();
            else
                Move(Target.position);
            
            TryToggleCombat(IsPlayerSpotted(_position), DistanceToToggleOnCombat, DistanceToToggleOffCombat);
        }
        
        protected int GetDamageValue() => Damage.GetDamageValue;

        protected void TryToggleCombat(bool isPlayerSpotted, float onDistance, float offDistance)
        {
            if (isPlayerSpotted && !Animator.GetBool(InCombat) && Vector3.Distance(_position, Player.localPosition) < onDistance)
            {
                ToggleCombatMode(true);
                _followPlayer = true;
            }
            else if (Vector3.Distance(_position, Player.localPosition) > offDistance || !_isPlayerAlive)
            {
                ToggleCombatMode(false);
            }
        }

        private void FlipWhileWalking()
        {
            if (Target.position == WaypointA.position)
                FlipSprite(true);
            else if (Target.position == WaypointB.position)
                FlipSprite(false);
        }

        private void FlipSprite(bool isFlip)
        {
            var currentDirection = transform.localScale;

            if (isFlip && currentDirection.x < 0 || !isFlip && currentDirection.x > 0) return;
            
            var flipped = new Vector2(-currentDirection.x, currentDirection.y);
            transform.localScale = flipped;
        }

        private void FaceToPlayer()
        {
            var direction = Player.localPosition - transform.position;

            FlipSprite(!(direction.x > 0));
        }

        private void TryChangeMoveDirection()
        {
            if (Vector2.Distance(transform.position, WaypointA.position) < 0.2f)
            {
                Target = WaypointB.transform;
                _followPlayer = false;
                ToggleIdleAnimation();
            }
            else if (Vector2.Distance(transform.position, WaypointB.position) < 0.2f)
            {
                Target = WaypointA.transform;
                _followPlayer = false;
                ToggleIdleAnimation();
            }
        }
        
        private void ToggleIdleAnimation()
        {
            Animator.SetTrigger(Idle);
        }

        protected virtual void Move(Vector2 position)
        {
           if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
           
            if (!_followPlayer || !_isPlayerAlive)
            {
                var target = new Vector2(position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            }
            else
            {
                var player = new Vector2(Player.transform.localPosition.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, player, Speed * FollowPlayertSpeedMultiplier * Time.deltaTime);
                FaceToPlayer();
            }
        }

        private void ToggleCombatMode(bool isCombat)
        {
            Animator.SetBool(InCombat, isCombat);
        }
        
        public virtual void ApplyDamage(int damage)
        {
            if (IsDead) return;

            if (Health > 0)
                Health -= damage;
            
            InstantiateDamageEffect();
            InstantiateDamageText(damage);
            
            if (!Animator.GetBool(InCombat))
                Animator.SetTrigger(Hit);
            
            ToggleCombatMode(true);
            _followPlayer = true;

            if (Health <= 0)
                Die();
        }

        private void InstantiateDamageText(int damage)
        {
            var spawnOffset = new Vector3(-0.2f, 0, 0);
            var damageText = Instantiate(DamageText, HitEffectSpawnPivot.position + spawnOffset, Quaternion.identity);
            var text = damageText.GetComponent<TextMeshPro>();
            
            text.text = $"-{damage}";
            StartCoroutine(AnimateText(damageText, text));
            
            Destroy(damageText, 0.9f);
        }

        private IEnumerator AnimateText(GameObject textObj, TextMeshPro text)
        {
            while (true)
            {
                if (textObj == null || text == null) yield break;
                
                textObj.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
                text.color -= new Color(0,0,0,0.01f); 
                yield return new WaitForSeconds(0.01f);
            }
        }
        
        protected virtual void InstantiateDamageEffect()
        {
            var direction = transform.localScale.x;
            var effect = Instantiate(HitEffectPrefab, HitEffectSpawnPivot);
            effect.transform.localScale *= direction * -1;
            Destroy(effect, 0.5f);
        }

        protected virtual bool IsPlayerSpotted(Vector2 startSpottingPosition)
        {
            if (!_isPlayerAlive) return false;
            
            Debug.DrawRay(startSpottingPosition, new Vector3(transform.localScale.x * SpotingRayDistance, 0,0));
            
            var hit = Physics2D.Raycast(startSpottingPosition, new Vector2(transform.localScale.x, 0), SpotingRayDistance, LayerMask.GetMask("PlayerHitbox"));

            return !ReferenceEquals(hit.collider, null);
        }

        private void Die()
        {
            Animator.SetTrigger(Death);

            SpawnDiamonds(_position);

            IsDead = true;
        }

        protected virtual void SpawnDiamonds(Vector2 spawnPosition)
        {
            for (int i = 0; i < Diamonds; i++)
            {
                var diamond = Instantiate(DiamondPrefab, spawnPosition, Quaternion.identity).GetComponent<Diamond>();
                diamond.SpawnFromEnemy();
            }
        }
    }
}
