using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimEvent : MonoBehaviour {

    public event Action OnAttackEnd;
    public event Action OnDead;

    public void AttackEnd() {
        OnAttackEnd?.Invoke();
    }

    public void Dead() {
        OnDead?.Invoke();
    }
}
