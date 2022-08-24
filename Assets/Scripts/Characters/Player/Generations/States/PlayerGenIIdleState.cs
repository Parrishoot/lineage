using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenIIdleState : PlayerIdleState<PlayerGenIController>
{
    public override void EnterState(PlayerGenIController controller)
    {
        base.EnterState(controller);
    }

    public override void UpdateState(PlayerGenIController controller)
    {
        base.UpdateState(controller);

        if(controller.weapon.IsAiming() && !GetMovementVector().Equals(Vector2.zero))
        {
            controller.SwitchState(controller.playerAimState);
        }
    }
}
