
public class LoseTargetTrigger : FSMTrigger {
    public override void Init() {
        TriggerID = FSMTriggerID.LoseTarget;
    }

    public override bool IsTrigger(FSMData data) {
        return data.LoseTarget();
    }
}
