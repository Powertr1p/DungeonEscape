using Interfaces;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IDamagable
    {
        private int _diamondsCount = 0;

        public int Diamonds
        {
            get => _diamondsCount;
            set
            {
                _diamondsCount = value;
                Diamonds = _diamondsCount;
            }
        }

        public void ApplyDamage(int damage)
        {
            Debug.Log("Player was attacked!");
        }
    }
}