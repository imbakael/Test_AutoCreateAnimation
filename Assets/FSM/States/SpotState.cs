using UnityEngine;

public class SpotState : FSMState {

    private float spotTime = 1f;
    private float duration;

    public override void Init() {
        StateID = FSMStateID.Spot;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter SpotState");
        data.LookAtPlayer();
        data.SpotAnim();
        data.IsSpotting = true;
        duration = 0f;
    }

    public override void Execute(FSMData data) {
        duration += Time.deltaTime;
        if (duration >= spotTime) {
            data.IsSpotting = false;
        }
    }
    
}
