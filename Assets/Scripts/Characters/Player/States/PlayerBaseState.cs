using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState<TStateMachine>: IState<TStateMachine>
    where TStateMachine: PlayerStateController<TStateMachine>
{

    public Vector2 GetMovementVector()
    {
        // Get the input
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Set movement vector
        return Vector2.ClampMagnitude(new Vector2(x, y), 1);
    }

    public abstract void EnterState(TStateMachine controller);
    public abstract void ExitState(TStateMachine controller);
    public abstract void FixedUpdateState(TStateMachine controller);
    public abstract void UpdateState(TStateMachine controller);
}
