using UnityEngine;

public class PursuitState : FSMState {

    public override void Init() {
        StateID = FSMStateID.Pursuit;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter PursuitState");
        data.PursuitAnim();
    }

    public override void Execute(FSMData data) {
        data.MoveTo();
    }
}
