using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowIdleState : BowBaseState
{
    public override void EnterState(PlayerBowController controller)
    {
        controller.SetInvisible();
        controller.animator.SetInteger(PlayerBowController.ANIMATION_STATE_NAME, 3);
    }

    public override void ExitState(PlayerBowController controller)
    {
        
    }

    public override void FixedUpdateState(PlayerBowController controller)
    {
        
    }

    public override void UpdateState(PlayerBowController controller)
    {
        if(InputManager.GetInstance().GetKeyDownWithCooldown(InputManager.ACTION.SHOOT))
        {
            controller.SwitchState(controller.bowChargingState);
        }
    }
}
