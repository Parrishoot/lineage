using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<TStateMachine> 
    where TStateMachine : StateMachine<TStateMachine>
{
    void EnterState(TStateMachine controller);

    void UpdateState(TStateMachine controller);

    void FixedUpdateState(TStateMachine controller);

    void ExitState(TStateMachine controller);
}
