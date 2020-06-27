using UnityEngine;

namespace Enemy
{
    public class MossGiant : Enemy
    {
        protected override void TryToggleCombat(bool isPlayerSpotted)
        {
            if (isPlayerSpotted && !Animator.GetBool(InCombat) && Vector3.Distance(transform.localPosition, Player.localPosition) < 1.5f)
                ToggleCombatMode(true);
            
            else if (!isPlayerSpotted && Vector3.Distance(transform.localPosition, Player.localPosition) < 3f)
                ToggleCombatMode(false);
        }
    }
}
