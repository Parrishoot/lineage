using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowFullyChargedState : BowBaseState
{
    public override void EnterState(PlayerBowController controller)
    {
        controller.animator.SetInteger(PlayerBowController.ANIMATION_STATE_NAME, 1);
    }

    public override void ExitState(PlayerBowController controller)
    {
        controller.SetColor(Color.white);
    }

    public override void FixedUpdateState(PlayerBowController controller)
    {
        
    }

    public override void UpdateState(PlayerBowController controller)
    {
        if(InputManager.GetInstance().GetKeyUp(InputManager.ACTION.SHOOT))
        {
            controller.SwitchState(controller.bowFiringState);
        }
    }
}
