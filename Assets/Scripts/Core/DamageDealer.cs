using UnityEngine;

namespace Core
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] protected int DamageValue = 1;
        public int GetDamageValue => DamageValue;
    }
}