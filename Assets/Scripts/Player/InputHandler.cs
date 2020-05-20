using System;
using UnityEngine;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        public event Action<float> OnMovementButtonPressed;
        public event Action OnJumpButtonPressed;
    
        private void Update()
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            OnMovementButtonPressed?.Invoke(horizontalInput);

            if (Input.GetKeyDown(KeyCode.Space))
                OnJumpButtonPressed?.Invoke();
        }
    }
}
