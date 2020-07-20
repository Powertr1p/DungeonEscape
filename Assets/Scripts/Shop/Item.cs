using Core;
using Interfaces;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "ShopItem")]
    public class Item : ScriptableObject, IBuyable
    {
        [SerializeField] private int _id;
        [SerializeField] private ItemType _itemType;
        
        public string ItemName;
        public int ItemPrice;
        
        public int GetId => _id;

        public enum ItemType
        {
            Sword,
            Boots,
            Key
        }
        
        public void Buy()
        {
            AddItemEffectsToPlayer();
        }
        
        private void AddItemEffectsToPlayer()
        {
            switch (_itemType)
            {
                case ItemType.Sword:
                    GameEventsHandler.Instance.FlameSwordBought();
                    break;
                case ItemType.Boots:
                    GameEventsHandler.Instance.BootsOfFlightBought();
                    break;
                case ItemType.Key:
                    GameEventsHandler.Instance.KeyBought();
                    break;
            }
        }
    }
}
