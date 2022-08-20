using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : IGenericUpdateBaseState<PlayerBaseState, PlayerStateController>
{

    public Vector2 GetMovementVector()
    {
        // Get the input
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Set movement vector
        return Vector2.ClampMagnitude(new Vector2(x, y), 1);
    }

    public abstract void EnterState(PlayerStateController controller);
    public abstract void ExitState(PlayerStateController controller);
    public abstract void FixedUpdateState(PlayerStateController controller);
    public abstract void UpdateState(PlayerStateController controller);
}
