using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Text _diamondsCount;
        [SerializeField] private Player.Player _player;

        private void OnEnable()
        {
            _player.DiamondsCountUpdated += UpdateDiamondsCount;
        }

        private void UpdateDiamondsCount()
        {
            _diamondsCount.text = _player.DiamondsCount.ToString();
        }

        private void OnDisable()
        {
            _player.DiamondsCountUpdated -= UpdateDiamondsCount;
        }
    }
}