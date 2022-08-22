using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenIAimState : PlayerBaseState<PlayerGenIController>
{

    public float movementSpeedReduceRate = 2f;

    public override void EnterState(PlayerGenIController controller)
    {
        controller.animator.SetInteger(PlayerMeta.ANIMATOR_MOVEMENT_PARAMETER_NAME, (int) PlayerGenIController.PLAYER_STATE.AIM);
    }

    public override void ExitState(PlayerGenIController controller)
    {
        
    }

    public override void FixedUpdateState(PlayerGenIController controller)
    {
        Vector2 movementVector = GetMovementVector();

        if (movementVector.Equals(Vector2.zero))
        {
            controller.SwitchState(controller.playerIdleState);
        }
        else if(!controller.bowController.IsAiming())
        {
            controller.SwitchState(controller.playerRunState);
        }

        controller.GetMover().Move(movementVector, controller.GetMover().baseMovementSpeed / movementSpeedReduceRate);
    }

    public override void UpdateState(PlayerGenIController controller)
    {
        if (InputManager.GetInstance().GetKeyDown(InputManager.ACTION.DASH))
        {
            controller.bowController.SwitchState(controller.bowController.bowIdleState);
            controller.SetDash(GetMovementVector());
        }
    }
}
