using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenIController : PlayerStateController<PlayerGenIController>
{
    public PlayerGenIAimState playerAimState;

    public override void Start()
    {
        base.Start();

        playerIdleState = new PlayerGenIIdleState();
        playerAimState = new PlayerGenIAimState();
        playerRunState = new PlayerGenIRunState();
        playerDashState = new PlayerGenIDashState(dashConfig);

        currentState = playerIdleState;

        SwitchState(playerIdleState);
    }
}
