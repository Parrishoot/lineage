using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowIdleState : BowBaseState
{
    public override void EnterState(BowController controller)
    {
        controller.SetInvisible();
        controller.animator.SetInteger(BowController.ANIMATION_STATE_NAME, 3);
    }

    public override void ExitState(BowController controller)
    {
        
    }

    public override void FixedUpdateState(BowController controller)
    {
        
    }

    public override void UpdateState(BowController controller)
    {
        if(InputManager.GetInstance().GetKeyDownWithCooldown(InputManager.ACTION.SHOOT))
        {
            controller.SwitchState(controller.bowChargingState);
        }
    }
}
