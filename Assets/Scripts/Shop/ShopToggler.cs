using System;
using Core;
using UnityEngine;

namespace Shop
{
    [RequireComponent(typeof(Collider2D))]
    public class ShopToggler : MonoBehaviour
    {
        [SerializeField] private GameObject _shopMenu;

        private Player.Player _player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                _player = player;
                ToggleShop(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ToggleShop(false);
            _player = null;
        }
        

        public void ToggleShop(bool isOpen)
        {
            _shopMenu.SetActive(isOpen);
            ShopDisplayUI.Instance.IsShopEnabled = isOpen;
            
            if (isOpen)
            {
                ShopDisplayUI.Instance.UpdatePlayerDiamonds();
                ShopDisplayUI.Instance.PrintItemsNameAndPrice();
            }
        }
    }
}
