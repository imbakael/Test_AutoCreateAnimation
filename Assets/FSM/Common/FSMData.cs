using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMData {
    private FSMBase fsm;
    public Npc npc;

    public float AttackInterval => npc.attackInterval;
    public bool IsSpotting { get; set; }

    public FSMData(FSMBase fsm) {
        this.fsm = fsm;
        npc = fsm.GetComponent<Npc>();
    }

    #region trigger
    public bool NoHealth() => npc.hp <= 0;

    public bool FindTarget() => npc.FindTarget(GameController.Instance.player.transform);

    public bool LoseTarget() => !FindTarget();

    public bool IsInAttackRange() => npc.IsInAttackRange(GameController.Instance.player.transform);

    public bool IsOutAttackRange() => !IsInAttackRange();
    #endregion

    #region 动画
    public void IdleAnim() => npc.IdleAnim();

    public void SpotAnim() => npc.SpotAnim();

    public void PursuitAnim() => npc.PursuitAnim();

    public void AttackAnim() => npc.AttackAnim();

    public void DeadAnim() {
        fsm.enabled = false;
        npc.DeadAnim();
    }
    #endregion

    public void IdleMoveTo(Vector2 target) => npc.IdleMoveTo(target);

    public void PursuitMoveTo(Vector2 target) => npc.PursuitMoveTo(target);

    public bool IsArrive(Vector2 target) => npc.IsArrive(target);

    public void LookAtPlayer() => npc.LookAt(GameController.Instance.player.transform.position);

}
