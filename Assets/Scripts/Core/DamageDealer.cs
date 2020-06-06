using UnityEngine;

namespace Core
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private int _damageValue = 1;
        public int GetDamageValue => _damageValue;
    }
}