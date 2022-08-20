using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<TState, TStatemachine> : MonoBehaviour
    where TState : IGenericUpdateBaseState<TState, TStatemachine>
    where TStatemachine: StateMachine<TState, TStatemachine>
{
    protected TState currentState;

    // Start is called before the first frame update
    public virtual void Start()
    {
        SwitchState(currentState);        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        currentState.UpdateState((TStatemachine) this);
    }

    public virtual void FixedUpdate()
    {
        currentState.FixedUpdateState((TStatemachine) this);
    }

    public void SwitchState(TState newState)
    {
       currentState.ExitState((TStatemachine) this);
       currentState = newState;
       currentState.EnterState((TStatemachine) this);
    }
}
