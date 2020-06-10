using Core;
using UnityEngine;

namespace Shop
{
    [RequireComponent(typeof(Collider2D))]
    public class ShopToggler : MonoBehaviour
    {
        [SerializeField] private GameObject _shopMenu;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                _shopMenu.SetActive(true); 
                ShopUIUpdater.Instance.UpdateDiamondsCount(player.DiamondsCount);
                ShopUIUpdater.Instance.PrintItemAttributes();
                ShopUIUpdater.Instance.IsShopEnabled = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _shopMenu.SetActive(false);
            ShopUIUpdater.Instance.IsShopEnabled = false;
        }
    }
}
