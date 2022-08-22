using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenIIdleState : PlayerIdleState<PlayerGenIController>
{
    public override void EnterState(PlayerGenIController controller)
    {
        Debug.Log("Gen I Rules!");

        base.EnterState(controller);
    }
}
