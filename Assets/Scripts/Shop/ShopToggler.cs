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
        public Player.Player GetPlayer() => _player;

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
                ShopDisplayUI.Instance.DisplayPlayerDiamonds(player.DiamondsCount);
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
            ShopDisplayUI.Instance.IsShopEnabled = isOpen;
        }
    }
}
