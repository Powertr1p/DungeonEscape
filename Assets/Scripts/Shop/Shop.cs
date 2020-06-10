using System;
using Core;
using UnityEngine;

namespace Shop
{
    [RequireComponent(typeof(ShopItems))]
    public class Shop : MonoBehaviour
    {
        public event Action<bool> ToggleShop;
        
        private ShopItems _itemsInStock;
        
        private int _upperItemYLinePosition = 164;
        private int _middleItemYLinePosition = 54;
        private int _downItemYLinePosition = -54;
        
        private int _currentSelectedItem;

        private void Awake()
        {
            _itemsInStock = GetComponent<ShopItems>();
        }

        public void SelectItem(int itemId)
        {
            switch (itemId)
            {
                case 0:
                    ShopUIUpdater.Instance.UpdateSelectionLinePosition(_upperItemYLinePosition);
                    break;
                case 1:
                    ShopUIUpdater.Instance.UpdateSelectionLinePosition(_middleItemYLinePosition);
                    break;
                case 2:
                    ShopUIUpdater.Instance.UpdateSelectionLinePosition(_downItemYLinePosition);
                    break;
            }

            _currentSelectedItem = itemId;
        }

        public void BuyItem()
        {
            var itemPrice = _itemsInStock.GetItemById(_currentSelectedItem).ItemPrice;
            TryConsumePlayerDiamonds(itemPrice);
        }

        private void TryConsumePlayerDiamonds(int itemPrice)
        {
            var player = GetComponentInChildren<ShopToggler>().GetCostumer();
            if (player == null) return;

            if (player.DiamondsCount >= itemPrice)
            {
                player.DiamondsCount -= itemPrice;
                Debug.Log("Purchased!");
            }
            else
            {
                ToggleShop?.Invoke(false);
            }
        }
        
        
    }
}
