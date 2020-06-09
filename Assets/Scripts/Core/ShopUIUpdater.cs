using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ShopUIUpdater : MonoBehaviour
    {
        private static ShopUIUpdater _instance;

        public static ShopUIUpdater UInstance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("Instance is null");
                }

                return _instance;
            }
        }

        [SerializeField] private Text _diamondsCount;
        [SerializeField] private Player.Player _player;

        private void Awake()
        {
            _instance = this;
        }

        private void Update()
        {
            _diamondsCount.text = _player.DiamondsCount.ToString();
        }
    }
}
