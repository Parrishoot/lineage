using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateController controller)
    {
        controller.animator.SetInteger(PlayerStateController.ANIMATOR_MOVEMENT_PARAMETER_NAME, (int) PlayerStateController.PLAYER_STATE.IDLE);
    }

    public override void ExitState(PlayerStateController controller)
    {
        
    }

    public override void FixedUpdateState(PlayerStateController controller)
    {
        Vector2 movementVector = GetMovementVector();

        if (!movementVector.Equals(Vector2.zero))
        {
            controller.SwitchState(controller.playerRunState);
            controller.sprintParticleSystem.Play();
        }
    }

    public override void UpdateState(PlayerStateController controller)
    {
        if (InputManager.GetInstance().GetKeyDown(InputManager.ACTION.DASH))
        {
            controller.SetDash(Vector2.right);
        }

        if (InputManager.GetInstance().GetKeyDown(InputManager.ACTION.INTERACT) && controller.GetInteractor().GetCurrentInteractable() != null)
        {
            controller.SwitchState(controller.playerInteractState);
        }
    }
}
