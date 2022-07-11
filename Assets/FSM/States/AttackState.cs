
using UnityEngine;

public class AttackState : FSMState {
    private FSMData data;

    public override void Init() {
        StateID = FSMStateID.Attack;
    }

    public override void Enter(FSMData data) {
        this.data = data;
        Debug.Log("Enter AttackState");
        data.Attack();
    }

    private void AttackEnd() {
        Debug.Log("npc 攻击结束");
        data.TurnUpdate();
    }

    public override void Exit(FSMData data) {
    
    }
}
