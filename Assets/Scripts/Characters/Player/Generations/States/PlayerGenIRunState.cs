using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenIRunState : PlayerRunState<PlayerGenIController>
{
    public override void EnterState(PlayerGenIController controller)
    {
        base.EnterState(controller);
    }

    public override void FixedUpdateState(PlayerGenIController controller)
    {
        if(controller.weapon.IsAiming())
        {
            controller.SwitchState(controller.playerAimState);
        }

        base.FixedUpdateState(controller);
    }
}
