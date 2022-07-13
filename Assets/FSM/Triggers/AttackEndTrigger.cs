using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEndTrigger : FSMTrigger {
    public override void Init() {
        TriggerID = FSMTriggerID.AttackEnd;
    }

    public override bool IsTrigger(FSMData data) {
        return data.npc.IsAttackEnd;
    }
}
