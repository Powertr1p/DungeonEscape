using System;
using Collectables;
using Core;
using UnityEngine;

namespace Enemy
{
    public class MossGiant : Enemy
    {
        private void FixedUpdate()
        {
            Debug.Log(Vector3.Distance(transform.localPosition, Player.localPosition));
        }

        protected override void TryToggleCombat(bool isPlayerSpotted)
        {
            if (isPlayerSpotted && !Animator.GetBool(InCombat) && Vector3.Distance(transform.localPosition, Player.localPosition) < 1.5f)
                ToggleCombatMode(true);
            
            else if (!isPlayerSpotted && Vector3.Distance(transform.localPosition, Player.localPosition) > 3f)
                ToggleCombatMode(false);
        }

        protected override bool IsPlayerSpotted()
        {
            if (!GameEventsHandler.Instance.IsPlayerAlive) return false;
            
            var offset = new Vector3(0, 0.8f);
            
            Debug.DrawRay(transform.position + offset, new Vector3(transform.localScale.x * SpotingRayDistance, 0,0));
            
            var hit = Physics2D.Raycast(transform.position + offset, new Vector2(transform.localScale.x, 0), SpotingRayDistance, LayerMask.GetMask("PlayerHitbox"));

            return !ReferenceEquals(hit.collider, null);
        }
        
        protected override void SpawnDiamonds()
        {
            for (int i = 0; i < Diamonds; i++)
            {
                var diamond = Instantiate(DiamondPrefab, HitEffectSpawnPivot.position, Quaternion.identity).GetComponent<Diamond>();
                diamond.SpawnFromEnemy();
            }
        }
    }
}
