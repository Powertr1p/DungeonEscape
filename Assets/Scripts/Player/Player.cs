using Interfaces;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IDamagable
    {
        public int DiamondsCount = 0;

        public void ApplyDamage(int damage)
        {
            Debug.Log("Player was attacked!");
        }
    }
}