using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowChargingState : BowBaseState
{

    private float currentCharge = 0f;

    public override void EnterState(PlayerBowController controller)
    {
        controller.SetVisible();
        controller.animator.SetInteger(PlayerBowController.ANIMATION_STATE_NAME, 0);

        currentCharge = 0f;
    }

    public override void ExitState(PlayerBowController controller)
    {
        
    }

    public override void FixedUpdateState(PlayerBowController controller)
    {

    }

    public override void UpdateState(PlayerBowController controller)
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
