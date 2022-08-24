using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState<TStateMachine> : PlayerBaseState<TStateMachine>
    where TStateMachine: PlayerStateController<TStateMachine>
{
    public override void EnterState(TStateMachine controller)
    {
        controller.animator.SetInteger(PlayerMeta.ANIMATOR_MOVEMENT_PARAMETER_NAME, (int) PlayerStateController<TStateMachine>.PLAYER_STATE.RUNNING);
    }

    public override void ExitState(TStateMachine controller)
    {
        
    }

    public override void FixedUpdateState(TStateMachine controller)
    {
        Vector2 movementVector = GetMovementVector();

        if (movementVector.Equals(Vector2.zero))
        {
            controller.SwitchState(controller.playerIdleState);
        }

        controller.GetMover().Move(movementVector);
    }

    public override void UpdateState(TStateMachine controller)
    {
        if (InputManager.GetInstance().GetKeyDown(InputManager.ACTION.DASH))
        {
            controller.SetDash(GetMovementVector());
        }

        if (InputManager.GetInstance().GetKeyDown(InputManager.ACTION.INTERACT) && controller.GetInteractor().GetCurrentInteractable() != null)
        {
            controller.SwitchState(controller.playerInteractState);
        }
    }
}
