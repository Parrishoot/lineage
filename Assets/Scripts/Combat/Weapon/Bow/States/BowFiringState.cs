using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowFiringState : BowBaseState
{
    public override void EnterState(BowController controller)
    {
        controller.Shoot();

        InputManager.GetInstance().SetKeyCooldown(InputManager.ACTION.SHOOT, controller.cooldown);

        controller.animator.SetInteger(BowController.ANIMATION_STATE_NAME, 2);
    }

    public override void ExitState(BowController controller)
    {

    }

    public override void FixedUpdateState(BowController controller)
    {

    }

    public override void UpdateState(BowController controller)
    {
        if(controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            controller.SwitchState(controller.bowIdleState);
        }
    }
}
