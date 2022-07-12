using UnityEngine;

public class IdleState : FSMState {

    public override void Init() {
        StateID = FSMStateID.Idle;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter IdleState");
        data.IdleAnim();
    }

    public override void Execute(FSMData data) {

    }
}
