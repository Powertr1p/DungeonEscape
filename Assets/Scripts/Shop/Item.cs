using System;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "ShopItem")]
    public class Item : ScriptableObject
    {
        [SerializeField] private int _id;
        
        public string ItemName;
        public int ItemPrice;
        
        public int GetId => _id;
    }
}
