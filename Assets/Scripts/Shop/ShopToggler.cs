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
        public Player.Player GetCostumer() => _player;

        private void OnEnable()
        {
            GetComponentInParent<Shop>().ToggleShop += ToggleShop;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                _player = player;
                
                ToggleShop(true); 
                ShopUIUpdater.Instance.UpdateDiamondsCount(player.DiamondsCount);
                ShopUIUpdater.Instance.PrintItemAttributes();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ToggleShop(false);

            _player = null;
        }

        private void ToggleShop(bool isOpen)
        {
            _shopMenu.SetActive(isOpen);
            ShopUIUpdater.Instance.IsShopEnabled = isOpen;
        }

        private void OnDisable()
        {
            GetComponentInParent<Shop>().ToggleShop -= ToggleShop;
        }
    }
}
