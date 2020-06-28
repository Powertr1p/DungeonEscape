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
            if (GameEventsHandler.Instance.PlayerDiamondsCount >= itemPrice)
            {
                GameEventsHandler.Instance.RemoveDiamonds(itemPrice);

                if (_currentSelectedItemId == GameEventsHandler.Instance.GetWinConditionItemId)
                    GameEventsHandler.Instance.HasWinCondition = true;
                
                else switch (_currentSelectedItemId)
                {
                    case 1:
                        GameEventsHandler.Instance.BootsOfFlightBought();
                        break;
                    case 0:
                        GameEventsHandler.Instance.FlameSwordBought();
                        break;
                }
            }
            else
            {
                Debug.Log("Not enough diamonds!");
            }

            ToggleShop?.Invoke(false);
        }
    }
}
