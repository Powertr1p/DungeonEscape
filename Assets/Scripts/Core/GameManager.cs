using System;
using Shop;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
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

        private void Awake()
        {
            _instance = this;
        }
    }
}
