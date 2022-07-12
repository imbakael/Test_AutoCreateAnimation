using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMData {
    private FSMBase fsm;
    private Npc npc;

    public float AttackInterval => npc.attakcInterval;
    public bool IsSpotting { get; set; }

    public FSMData(FSMBase fsm) {
        this.fsm = fsm;
        npc = fsm.GetComponent<Npc>();
    }

    public bool NoHealth() => npc.hp <= 0;

    public bool FindTarget() => npc.FindTarget(GameController.Instance.player.transform);

    public bool LoseTarget() => !FindTarget();

    public bool IsInAttackRange() => npc.IsInAttackRange(GameController.Instance.player.transform);

    public bool IsOutAttackRange() => !IsInAttackRange();

    public void IdleAnim() { }

    public void SpotAnim() { }

    public void PursuitAnim() { }

    public void AttackAnim() { }

    public void DeadAnim() {
        fsm.enabled = false;
    }

    public void MoveTo() => npc.MoveTo(GameController.Instance.player.transform.position);

}
