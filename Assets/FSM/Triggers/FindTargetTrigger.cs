
public class FindTargetTrigger : FSMTrigger {
    public override void Init() {
        TriggerID = FSMTriggerID.FindTarget;
    }

    public override bool IsTrigger(FSMData data) {
        return data.FindTarget();
    }
}
