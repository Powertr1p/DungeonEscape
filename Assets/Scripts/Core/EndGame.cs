using Interfaces;
using UnityEngine;

namespace Core
{
    public class EndGame : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!GameEventsHandler.Instance.HasWinCondition) return;
            
            other.gameObject.GetComponent<IDamagable>().ApplyDamage(999);
        }
    }
}
