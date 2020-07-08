using System;
using System.Reflection;
using Core;
using UnityEngine;

namespace Shop
{
    [RequireComponent(typeof(ShopItems))]
    public class Shop : MonoBehaviour
    {
        public event Action<Item> OnItemBought;
        public event Action OnItemBuyFailed;

        private ShopItems _itemsInStock;

        private int _upperItemYLinePosition = 164;
        private int _middleItemYLinePosition = 54;
        private int _downItemYLinePosition = -54;
        
        private int? _currentSelectedItemId;

        private void Awake()
        {
            _itemsInStock = GetComponent<ShopItems>();
        }

        private void Start()
        {
            OnItemBought = (item) =>
            {
                GameEventsHandler.Instance.RemoveDiamonds(item.ItemPrice);
                ShopDisplayUI.Instance.UpdatePlayerDiamonds();
                ShopDisplayUI.Instance.ShowSuccessItemBoughtItemMessage(item.ItemName);
                ShopDisplayUI.Instance.ToggleOffBoughtItem(item.GetId);
                _currentSelectedItemId = null;
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
            if (_currentSelectedItemId == null) return;
            
            var selectedItem = _itemsInStock.GetItemById(_currentSelectedItemId);
            TryConsumePlayerDiamonds(selectedItem);
        }

        private void BuyItem(Item item) //TODO: REFACTOR THIS SHIT
        {
            if (GameEventsHandler.Instance.IsBuyingBlocked || _currentSelectedItemId == null) return;
            
            switch (_currentSelectedItemId)
            {
                case 0:
                    GameEventsHandler.Instance.FlameSwordBought();
                    break;
                case 1:
                    GameEventsHandler.Instance.BootsOfFlightBought();
                    break;
                case 2:
                    GameEventsHandler.Instance.KeyBought();
                    break;
            }
            
            OnItemBought?.Invoke(item);
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
