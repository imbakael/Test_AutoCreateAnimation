using UnityEngine;

public class SpotState : FSMState {

    private FSMData data;

    public override void Init() {
        StateID = FSMStateID.Spot;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter SpotState");
        
    }
}
