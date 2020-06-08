using Interfaces;
using UnityEngine;

namespace Collectables
{
    public class Diamond : MonoBehaviour, ICollectable
    {
        [SerializeField] private int _diamondValue = 1;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player.Player player))
                Collect(_diamondValue, player);
        }
        
        public void Collect(int value, Player.Player player)
        {
            player.Diamonds += value;
            Destroy(gameObject);
        }
    }
}
