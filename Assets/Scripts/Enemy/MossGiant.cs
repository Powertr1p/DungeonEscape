using System;
using Collectables;
using Core;
using UnityEngine;

namespace Enemy
{
    public class MossGiant : Enemy
    {
        protected override void TryToggleCombat(bool isPlayerSpotted)
        {
            if (isPlayerSpotted && !Animator.GetBool(InCombat) && Vector3.Distance(transform.localPosition, Player.localPosition) < 1.5f)
                ToggleCombatMode(true);
            
            else if (!isPlayerSpotted && Vector3.Distance(transform.localPosition, Player.localPosition) > 2.5f)
                ToggleCombatMode(false);
        }

        protected override bool IsPlayerSpotted(Vector2 startSpottingPosition)
        {
            var offset = new Vector3(0, 0.8f);
            return base.IsPlayerSpotted(transform.position + offset);
        }
        
        protected override void SpawnDiamonds(Vector2 spawnPosition)
        {
            base.SpawnDiamonds(HitEffectSpawnPivot.position);
        }
    }
}
