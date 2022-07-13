using UnityEngine;

public class AttackState : FSMState {

    private float attackTime = 0f;

    public override void Init() {
        StateID = FSMStateID.Attack;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter AttackState");
        data.npc.IsAttackEnd = false;
    }

    public override void Execute(FSMData data) {
        if (attackTime <= Time.time) {
            data.LookAtPlayer();
            data.AttackAnim();
            attackTime = Time.time + data.AttackInterval;
        }
    }

}
