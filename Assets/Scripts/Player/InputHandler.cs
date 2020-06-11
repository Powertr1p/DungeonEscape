using System;
using UnityEngine;

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
            
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            OnMovementButtonPressed?.Invoke(horizontalInput);

            if (Input.GetKeyDown(KeyCode.Space))
                OnJumpButtonPressed?.Invoke();

            if (Input.GetKeyDown(KeyCode.Mouse0))
                OnAttackButtonPressed?.Invoke();
        }
    }
}
