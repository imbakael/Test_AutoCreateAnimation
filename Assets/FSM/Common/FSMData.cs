using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMData {
    private FSMBase fsm;

    public FSMData(FSMBase fsm) {
        this.fsm = fsm;
    }

    public bool NoHealth() => false;

    public bool FindTarget() => false;

    public bool LoseTarget() => !FindTarget();

    public bool IsInAttackRange() => false;

    public bool IsOutAttackRange() => !IsInAttackRange();

    public void Attack() { }

    public void SetIdle() { }

    public void Move() { }

    public void Dead() {
        fsm.enabled = false;
    }

    public void TurnUpdate() => fsm.TurnUpdate();
}
