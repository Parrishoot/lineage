using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowFiringState : BowBaseState
{
    public override void EnterState(BowController controller)
    {
        Debug.Log("Firing!");

        InputManager.GetInstance().SetKeyCooldown(InputManager.ACTION.SHOOT, controller.cooldown);

        controller.SwitchState(controller.bowIdleState);
    }

    public override void ExitState(BowController controller)
    {

    }

    public override void FixedUpdateState(BowController controller)
    {

    }

    public override void UpdateState(BowController controller)
    {

    }
}
