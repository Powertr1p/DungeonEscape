using Interfaces;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IDamagable
    {
        public void ApplyDamage(int damage)
        {
            Debug.Log("Player was attacked!");
        }
    }
}