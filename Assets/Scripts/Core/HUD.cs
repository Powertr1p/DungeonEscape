using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Text _diamondsCount;
        [SerializeField] private Image[] _livesUI;
        [SerializeField] private Player.Player _player;

        private void OnEnable()
        {
            _player.DamageTaken += UpdateLivesRemaining;
        }

        private void Start()
        {
            GameManager.Instance.DiamondsCountUpdated += UpdateDiamondsCount;
        }

        private void UpdateDiamondsCount()
        {
            _diamondsCount.text = _player.DiamondsCount.ToString();
        }

        private void UpdateLivesRemaining(int livesRemaining)
        {
            _livesUI[livesRemaining].color = new Color32(255,255,255,50);
        }

        private void OnDisable()
        {
            _player.DamageTaken -= UpdateLivesRemaining;
            GameManager.Instance.DiamondsCountUpdated -= UpdateDiamondsCount;
        }
    }
}