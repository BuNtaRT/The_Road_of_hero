using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public event Action OnDamage;
    public void Damage() 
    {
        OnDamage?.Invoke();
    }
}
