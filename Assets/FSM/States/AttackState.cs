using UnityEngine;

public class AttackState : FSMState {

    private float attackTime;

    public override void Init() {
        StateID = FSMStateID.Attack;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter AttackState");
        attackTime = 0f;
    }

    public override void Execute(FSMData data) {
        if (attackTime <= Time.time) {
            data.AttackAnim();
            attackTime = Time.time + data.AttackInterval;
        }
    }

}
