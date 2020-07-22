using UnityEngine;

namespace Enemy
{
    public class MossGiant : Enemy
    {
        [SerializeField] private Vector3 _spottingRayOffset = new Vector3(0f,0.8f);

        protected override void PlayDamageSound()
        {
            throw new System.NotImplementedException();
        }

        protected override void PlayDeathSound()
        {
            throw new System.NotImplementedException();
        }

        protected override bool IsPlayerSpotted(Vector2 startSpottingPosition)
        {
            return base.IsPlayerSpotted(transform.position + _spottingRayOffset);
        }
        
        protected override void SpawnDiamonds(Vector2 spawnPosition)
        {
            base.SpawnDiamonds(HitEffectSpawnPivot.position);
        }
    }
}
