using System;
using System.Reflection;
using Core;
using UnityEngine;

namespace Shop
{
    [RequireComponent(typeof(ShopItems))]
    public class Shop : MonoBehaviour
    {
        public event Action<int, string> OnItemBought;
        public event Action OnItemBuyFailed;

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

            OnItemBuyFailed = () =>
            {
                ShopDisplayUI.Instance.ShowNotEnoughDiamodsMessage();
            };
        }

        public void SelectItem(int itemId)
        {
            ShopDisplayUI.Instance.UpdateSelectionLinePosition(itemId);
            _currentSelectedItemId = itemId;
        }

        public void TryBuyItem()
        {
            var selectedItem = _itemsInStock.GetItemById(_currentSelectedItemId);
            TryConsumePlayerDiamonds(selectedItem);
        }

        private void BuyItem(Item item)
        {
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
            
            OnItemBought?.Invoke(item.ItemPrice, item.ItemName);
        }

        private void TryConsumePlayerDiamonds(Item item)
        {
            var diamondsAmount = GameEventsHandler.Instance.PlayerDiamondsCount;
            
            if (diamondsAmount >= item.ItemPrice)
               BuyItem(item);
            else
                OnItemBuyFailed?.Invoke();
        }
    }
}
