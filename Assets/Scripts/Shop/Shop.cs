using System;
using System.Reflection;
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
        
        private int _currentSelectedItemId;

        private void Awake()
        {
            _itemsInStock = GetComponent<ShopItems>();
        }

        public void SelectItem(int itemId)
        {
            ShopDisplayUI.Instance.UpdateSelectionLinePosition(itemId);
            _currentSelectedItemId = itemId;
        }

        public void TryBuyItem()
        {
            var itemPrice = _itemsInStock.GetItemById(_currentSelectedItemId).ItemPrice;
            TryConsumePlayerDiamonds(itemPrice);
        }

        private void TryConsumePlayerDiamonds(int itemPrice)
        {
            var player = GetComponentInChildren<ShopToggler>().GetPlayer();
            if (player == null) return;

            if (player.DiamondsCount >= itemPrice)
            {
                player.DiamondsCount -= itemPrice;
                
                if (_currentSelectedItemId == GameManager.Instance.GetWinConditionItemId)
                    GameManager.Instance.HasWinCondition = true;
            }
            else
            {
                Debug.Log("Not enough diamonds!");
            }

            ToggleShop?.Invoke(false);
        }
    }
}
