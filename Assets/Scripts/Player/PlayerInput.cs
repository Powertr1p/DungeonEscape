using System;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action<float> OnMovementButtonPressed;
    
        private void Update()
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            OnMovementButtonPressed?.Invoke(horizontalInput);
        }
    }
}
