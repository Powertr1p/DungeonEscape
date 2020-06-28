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
            _player.Died += OnDied;
        }

        private void Start()
        {
            GameEventsHandler.Instance.DiamondsCountUpdated += UpdateDiamondsCount;
        }

        private void UpdateDiamondsCount()
        {
            _diamondsCount.text = GameEventsHandler.Instance.PlayerDiamondsCount.ToString();
        }

        private void UpdateLivesRemaining(int livesRemaining)
        {
            if (livesRemaining < 0) return;
            
            _livesUI[livesRemaining].color = new Color32(255,255,255,50);
        }

        private void OnDied()
        {
            foreach (var section in _livesUI)
            {
                section.color = new Color32(255,255,255,50);
            }
        }

        private void OnDisable()
        {
            _player.DamageTaken -= UpdateLivesRemaining;
            GameEventsHandler.Instance.DiamondsCountUpdated -= UpdateDiamondsCount;
        }
    }
}