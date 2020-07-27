using Interfaces;
using UnityEngine;

namespace Hazards
{
    [RequireComponent(typeof(Collider2D))]
    public class Spikes: MonoBehaviour
    {
        private const int FatalDamage = 999;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamagable entity))
            {
                entity.ApplyDamage(FatalDamage);
            }
        }
    }
}
