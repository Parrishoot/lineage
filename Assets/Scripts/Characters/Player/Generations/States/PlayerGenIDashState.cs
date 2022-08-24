using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenIDashState : PlayerDashState<PlayerGenIController>
{
    public PlayerGenIDashState(DashConfig dashConfig) : base(dashConfig) { }

    public override void EnterState(PlayerGenIController controller)
    {
        controller.weapon.Reset();

        base.EnterState(controller);
    }
}
