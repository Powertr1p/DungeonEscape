using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        public event Action<float> OnMovementButtonPressed;
        public event Action OnJumpButtonPressed;
        public event Action OnAttackButtonPressed;

        private Player _player;
        
        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            if (!_player.IsAlive) return;

            var horizontalInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            Debug.Log(horizontalInput);
            OnMovementButtonPressed?.Invoke(horizontalInput);

            if (CrossPlatformInputManager.GetButtonDown("B_Button"))
                OnJumpButtonPressed?.Invoke();

            if (CrossPlatformInputManager.GetButtonDown("A_Button"))
                OnAttackButtonPressed?.Invoke();
        }
    }
}
