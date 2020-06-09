using System;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Collider2D))]
    public class ShopKeeper : MonoBehaviour
    {
        [SerializeField] private GameObject _shopMenu;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                _shopMenu.SetActive(true); 
                ShopUIUpdater.Instance.OpenShop(player.DiamondsCount);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _shopMenu.SetActive(false);
        }
    }
}
