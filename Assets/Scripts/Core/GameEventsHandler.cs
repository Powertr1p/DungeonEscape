using System;
using Shop;
using UnityEngine;

namespace Core
{
    public class GameEventsHandler : MonoBehaviour
    {
        public event Action DiamondsCountUpdated;
        public event Action OnBootsOfLightBought;
        public event Action OnFlameSwordBought;
        
        [SerializeField] private Player.Player _player;
        [SerializeField] private Item _winCondotionItem;

        private static GameEventsHandler _instance;
        public static GameEventsHandler Instance => _instance;

        public int GetWinConditionItemId => _winCondotionItem.GetId;
        public int PlayerDiamondsCount => _player.DiamondsCount;
        public bool IsPlayerAlive => _player.IsAlive;
        
        public bool HasWinCondition { get; set; }

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            DiamondsCountUpdated?.Invoke();
        }

        public void AddDiamonds(int amount)
        {
            _player.DiamondsCount += amount;
            DiamondsCountUpdated?.Invoke();
        }
        
        public void RemoveDiamonds(int amount)
        {
            _player.DiamondsCount -= amount;
            DiamondsCountUpdated?.Invoke();
        }

        public void BootsOfFlightBought()
        {
            OnBootsOfLightBought?.Invoke();
        }

        public void FlameSwordBought()
        {
            OnFlameSwordBought?.Invoke();
        }
        
    }
}
