using UnityEngine;

namespace Player
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        public int WeaponDamage => _damage;
    }
}