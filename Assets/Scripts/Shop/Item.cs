using System;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "ShopItems")]
    public class Item : ScriptableObject
    {
        public string ItemName;
        public int ItemPrice;
        
        private static int _id;
        public int GetId => _id;

        private void OnEnable()
        {
            ++_id;
        }
    }
}
