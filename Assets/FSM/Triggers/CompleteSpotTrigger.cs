
public class CompleteSpotTrigger : FSMTrigger {
    public override void Init() {
        TriggerID = FSMTriggerID.CompleteSpot;
    }

    public override bool IsTrigger(FSMData data) {
        return data.IsSpotting == false;
    }
}
