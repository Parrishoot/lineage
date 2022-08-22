using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenIRunState : PlayerRunState<PlayerGenIController>
{
    public override void FixedUpdateState(PlayerGenIController controller)
    {
        if(controller.bowController.IsAiming())
        {
            controller.SwitchState(controller.playerAimState);
        }

        base.FixedUpdateState(controller);
    }
}
