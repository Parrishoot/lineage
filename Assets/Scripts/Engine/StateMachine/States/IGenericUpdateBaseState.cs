using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGenericUpdateBaseState<TState, TStateMachine> 
    where TState : IGenericUpdateBaseState<TState, TStateMachine>
    where TStateMachine : StateMachine<TState, TStateMachine>
{
    void EnterState(TStateMachine controller);

    void UpdateState(TStateMachine controller);

    void FixedUpdateState(TStateMachine controller);

    void ExitState(TStateMachine controller);
}
