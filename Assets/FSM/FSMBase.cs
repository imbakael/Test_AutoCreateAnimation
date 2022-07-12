using System.Collections.Generic;
using UnityEngine;

public class FSMBase : MonoBehaviour
{
    [SerializeField] private FSMStateID defaultStateID = default;

    public FSMData FsmData { get; private set; }

    public FSMStateID test_currentStateID;

    private List<FSMState> states;
    private FSMState defaultState;
    private FSMState currentState;

    private void Start() {
        FsmData = new FSMData(this);
        ConfigFSM();
        InitDefaultState();
    }

    private void ConfigFSM() {
        states = new List<FSMState>();

        var idle = new IdleState();
        idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        idle.AddMap(FSMTriggerID.InAttackRange, FSMStateID.Attack);
        idle.AddMap(FSMTriggerID.FindTarget, FSMStateID.Spot);
        states.Add(idle);

        var spot = new SpotState();
        spot.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        spot.AddMap(FSMTriggerID.CompleteSpot, FSMStateID.Pursuit);
        states.Add(spot);

        var pursuit = new PursuitState();
        pursuit.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        pursuit.AddMap(FSMTriggerID.InAttackRange, FSMStateID.Attack);
        pursuit.AddMap(FSMTriggerID.LoseTarget, FSMStateID.Idle);
        states.Add(pursuit);

        var attack = new AttackState();
        attack.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        attack.AddMap(FSMTriggerID.OutAttackRange, FSMStateID.Pursuit);
        states.Add(attack);

        var dead = new DeadState();
        states.Add(dead);
    }

    private void InitDefaultState() {
        defaultState = states.Find(t => t.StateID == defaultStateID);
        currentState = defaultState;
        currentState.Enter(FsmData);
    }

    private void Update() {
        currentState.Check(this);
        currentState.Execute(FsmData);
    }

    public void ChangeState(FSMStateID stateID) {
        FSMState nextState = stateID == FSMStateID.Default ? defaultState : states.Find(t => t.StateID == stateID);
        if (nextState == currentState) {
            return;
        }
        currentState.Exit(FsmData);
        currentState = nextState;
        currentState.Enter(FsmData);
        test_currentStateID = currentState.StateID;
    }
}
