using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowFullyChargedState : BowBaseState
{
    public override void EnterState(BowController controller)
    {
        controller.animator.SetInteger(BowController.ANIMATION_STATE_NAME, 1);
    }

    public override void ExitState(BowController controller)
    {
        controller.SetColor(Color.white);
    }

    public override void FixedUpdateState(BowController controller)
    {
        
    }

    public override void UpdateState(BowController controller)
    {
        if(InputManager.GetInstance().GetKeyUp(InputManager.ACTION.SHOOT))
        {
            controller.SwitchState(controller.bowFiringState);
        }
    }
}
