
namespace Enemy
{
    public class Skeleton : Enemy
    {
        protected override void Init()
        {
            base.Init();
            HitEffectSpawnPivot = transform;
        }
    }
}
