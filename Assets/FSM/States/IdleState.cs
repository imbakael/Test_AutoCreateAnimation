using UnityEngine;

public class IdleState : FSMState {

    private Vector2 currentTarget;

    public override void Init() {
        StateID = FSMStateID.Idle;
    }

    public override void Enter(FSMData data) {
        Debug.Log("Enter IdleState");
        data.IdleAnim();
        currentTarget = GetRandomPos();
    }

    public override void Execute(FSMData data) {
        if (data.IsArrive(currentTarget)) {
            currentTarget = GetOtherTarget();
        }
        data.IdleMoveTo(currentTarget);
    }

    private Vector2 GetOtherTarget() {
        Vector2 newTarget = GetRandomPos();
        while (newTarget == currentTarget) {
            newTarget = GetRandomPos();
        }
        return newTarget;
    }

    private Vector2 GetRandomPos() {
        return GameController.Instance.points[Random.Range(0, GameController.Instance.points.Length)].position;
    }
}
