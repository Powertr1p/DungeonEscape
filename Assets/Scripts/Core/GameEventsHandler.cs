using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    public class GameEventsHandler : MonoBehaviour
    {
        public event Action DiamondsCountUpdated;
        public event Action OnBootsOfLightBought;
        public event Action OnFlameSwordBought;
        public event Action OnKeyBought;
        
        [SerializeField] private Player.Player _player;

        private static GameEventsHandler _instance;
        public static GameEventsHandler Instance => _instance;

        public bool IsPlayerAlive => _player.IsAlive;
        public bool IsBuyingBlocked { get; private set; }
        public bool HasWinCondition { get; private set; }
        public int PlayerDiamondsCount => _player.DiamondsCount;

        private static int _playerDeathCount = 0;
        public int PlayerDeathCount => _playerDeathCount;

        private void Awake() => _instance = this;

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
            StartCoroutine(StartRemovingDiamonds(amount));
        }

        public void BootsOfFlightBought()
        {
            OnBootsOfLightBought?.Invoke();
        }

        public void FlameSwordBought()
        {
            OnFlameSwordBought?.Invoke();
        }

        public void KeyBought()
        {
            HasWinCondition = true;
            OnKeyBought?.Invoke();
        }

        private IEnumerator StartRemovingDiamonds(int amount)
        {
            var time = 0.5f;
            var decreaseRate = 0.1f;
            var diamondsToMinus = 1;
           
            IsBuyingBlocked = true; 
            
            do
            {
                yield return new WaitForSecondsRealtime(0);
                _player.DiamondsCount -= diamondsToMinus;
                amount -= diamondsToMinus;
                DiamondsCountUpdated?.Invoke();
            } while (amount != 0);

            IsBuyingBlocked = false;
        }

        public void CountPlayerDeath()
        {
            _playerDeathCount++;
        }
    }
}
