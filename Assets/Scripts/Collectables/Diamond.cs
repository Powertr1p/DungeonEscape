using Interfaces;
using UnityEngine;

namespace Collectables
{
    public class Diamond : MonoBehaviour, ICollectible
    {
        public int DiamondValue = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player.Player player))
                Collect(DiamondValue, player);
        }
        
        public void Collect(int value, Player.Player player)
        {
            player.AddDiamonds(value);
            Destroy(gameObject);
        }
    }
}
