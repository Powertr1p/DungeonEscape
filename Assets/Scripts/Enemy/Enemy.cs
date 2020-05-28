using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int Health;
    [SerializeField] protected float Speed;
    [SerializeField] protected int Gems;

    protected virtual void Attack()
    {
        
    }

    protected abstract void Update();
}
