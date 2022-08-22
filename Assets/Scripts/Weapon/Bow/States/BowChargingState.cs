using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowChargingState : BowBaseState
{

    private float currentCharge = 0f;

    public override void EnterState(BowController controller)
    {
        controller.SetVisible();
        controller.animator.SetInteger(BowController.ANIMATION_STATE_NAME, 0);

        currentCharge = 0f;
    }

    public override void ExitState(BowController controller)
    {
        
    }

    public override void FixedUpdateState(BowController controller)
    {

    }

    public override void UpdateState(BowController controller)
    {

        currentCharge += Time.deltaTime;

        if(currentCharge >= controller.timeToCharge)
        {
            controller.SwitchState(controller.bowFullyChargedState);
        }

        if(InputManager.GetInstance().GetKeyUp(InputManager.ACTION.SHOOT))
        {
            controller.SwitchState(controller.bowFiringState);
        }
    }
}
