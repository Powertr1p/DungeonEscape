﻿using System;
using Core;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        public event Action<float> OnMovementButtonPressed;
        public event Action OnJumpButtonPressed;
        public event Action OnAttackButtonPressed;
        
        private void Update()
        {
            if (!GameEventsHandler.Instance.IsPlayerAlive) return;
            
            var horizontalInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            OnMovementButtonPressed?.Invoke(horizontalInput);

            if (CrossPlatformInputManager.GetButtonDown("B_Button")) 
                OnJumpButtonPressed?.Invoke();

            if (CrossPlatformInputManager.GetButtonDown("A_Button"))
                OnAttackButtonPressed?.Invoke();
        }
    }
}
