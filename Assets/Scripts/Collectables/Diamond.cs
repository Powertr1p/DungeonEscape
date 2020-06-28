using Core;
using Interfaces;
using UnityEngine;

namespace Collectables
{
    public class Diamond : MonoBehaviour, ICollectible
    {
        public int DiamondValue = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Player.Player>())
                Collect(DiamondValue);
        }
        
        public void Collect(int value)
        {
            GameEventsHandler.Instance.AddDiamonds(value);
            Destroy(gameObject);
        }
    }
}
