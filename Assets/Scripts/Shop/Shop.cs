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
        public event Action<int, string> OnItemBought;

        private ShopItems _itemsInStock;

        private int _upperItemYLinePosition = 164;
        private int _middleItemYLinePosition = 54;
        private int _downItemYLinePosition = -54;
        
        private int _currentSelectedItemId;

        private void Awake()
        {
            _itemsInStock = GetComponent<ShopItems>();
        }

        private void Start()
        {
            OnItemBought = (price, name) =>
            {
                GameEventsHandler.Instance.RemoveDiamonds(price);
                ShopDisplayUI.Instance.UpdatePlayerDiamonds();
                ShopDisplayUI.Instance.ShowSuccessItemBoughtItemMessage(name);
            };
        }

        public void SelectItem(int itemId)
        {
            ShopDisplayUI.Instance.UpdateSelectionLinePosition(itemId);
            _currentSelectedItemId = itemId;
        }

        public void TryBuyItem()
        {
            var itemPrice = _itemsInStock.GetItemById(_currentSelectedItemId).ItemPrice;
            var itemName = _itemsInStock.GetItemById(_currentSelectedItemId).ItemName;
            TryConsumePlayerDiamonds(itemPrice, itemName);
        }

        private void TryConsumePlayerDiamonds(int itemPrice, string itemName)
        {
            if (GameEventsHandler.Instance.PlayerDiamondsCount >= itemPrice)
            {
                OnItemBought?.Invoke(itemPrice, itemName);

                switch (_currentSelectedItemId)
                {
                    case 1:
                        GameEventsHandler.Instance.BootsOfFlightBought();
                        break;
                    case 0:
                        GameEventsHandler.Instance.FlameSwordBought();
                        break;
                    case 2:
                        GameEventsHandler.Instance.KeyBought();
                        break;
                }
            }
            else
            {
                Debug.Log("Not enough diamonds!");
            }
        }
    }
}
