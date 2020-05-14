using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<float> OnMovementButtonPressed;
    
    private void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        OnMovementButtonPressed?.Invoke(horizontalInput);
    }
}
