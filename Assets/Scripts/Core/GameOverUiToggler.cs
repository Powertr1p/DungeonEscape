using UnityEngine;

namespace Core
{
    public class GameOverUiToggler : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverUi;
        [SerializeField] Player.Player _player;

        private void OnEnable()
        {
            _player.Died += ToggleUI;
        }

        private void ToggleUI()
        {
            _gameOverUi.SetActive(true);
        }

        private void OnDisable()
        {
            _player.Died -= ToggleUI;
        }
    }
}
