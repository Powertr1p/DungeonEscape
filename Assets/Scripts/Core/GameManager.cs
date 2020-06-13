using System;
using Shop;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public event Action DiamondsCountUpdated;
        
        [SerializeField] private Player.Player _player;
        [SerializeField] private Item _winCondotionItem;

        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("GameManager is null");
                return _instance;
            }
        }

        public int GetWinConditionItemId => _winCondotionItem.GetId;
        
        public bool HasWinCondition { get; set; }

        public int PlayerDiamondsCount => _player.DiamondsCount;

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
    }
}
