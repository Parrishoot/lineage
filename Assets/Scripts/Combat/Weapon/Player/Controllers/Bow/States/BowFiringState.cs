using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowFiringState : BowBaseState
{
    public override void EnterState(PlayerBowController controller)
    {
        controller.Shoot();

        InputManager.GetInstance().SetKeyCooldown(InputManager.ACTION.SHOOT, controller.cooldown);

        controller.animator.SetInteger(PlayerBowController.ANIMATION_STATE_NAME, 2);
    }

    public override void ExitState(PlayerBowController controller)
    {

    }

    public override void FixedUpdateState(PlayerBowController controller)
    {

    }

    public override void UpdateState(PlayerBowController controller)
    {
        if(controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            controller.SwitchState(controller.bowIdleState);
        }
    }
}
