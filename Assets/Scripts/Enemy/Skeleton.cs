
namespace Enemy
{
    public class Skeleton : Enemy
    {
        protected override void PlayDamageSound()
        {
            throw new System.NotImplementedException();
        }

        protected override void PlayDeathSound()
        {
            throw new System.NotImplementedException();
        }

        protected override void Init()
        {
            base.Init();
            HitEffectSpawnPivot = transform;
        }
    }
}
