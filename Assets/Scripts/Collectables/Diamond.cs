using Interfaces;
using UnityEngine;

namespace Collectables
{
    public class Diamond : MonoBehaviour, ICollectable
    {
        [SerializeField] private int _diamondValue = 1;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
        }

        public void Collect(int value)
        {
        }
    }
}
