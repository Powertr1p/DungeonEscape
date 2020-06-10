using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class ShopItems : MonoBehaviour
    {
        [SerializeField] private Item[] _shopItems;
        
        private readonly List<Item> _itemsInStock = new List<Item>();

        private void Start()
        {
            foreach (var item in _shopItems)
            {
                AddItem(item);
            }
        }

        private void AddItem(Item item)
        {
            _itemsInStock.Add(item);
        }

        public Item GetItemById(int id)
        {
            return _itemsInStock.Find(x => x.GetId == id);
        }
    }
}
