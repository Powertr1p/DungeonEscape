using UnityEngine;

namespace Enemy
{
    public class MossGiant : Enemy
    {
        protected override void TryToggleCombat(bool IsPlayerSpotted)
        {
            if (IsPlayerSpotted && !Animator.GetBool(InCombat) && Vector3.Distance(transform.localPosition, Player.localPosition) < 1.5f)
            {
                ToggleCombatMode(IsPlayerSpotted);
                Debug.Log("Toggled");
            }
            else if (!IsPlayerSpotted && Vector3.Distance(transform.localPosition, Player.localPosition) < 3f)
            {
                ToggleCombatMode(IsPlayerSpotted);
            }
        }
    }
}
