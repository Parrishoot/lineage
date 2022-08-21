using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowFullyChargedState : BowBaseState
{
    public override void EnterState(BowController controller)
    {
        Debug.Log("Fully Charged!");

        controller.SetColor(Color.red);
    }

    public override void ExitState(BowController controller)
    {
        controller.SetColor(Color.white);
    }

    public override void FixedUpdateState(BowController controller)
    {
        
    }

    public override void UpdateState(BowController controller)
    {
        if(InputManager.GetInstance().GetKeyUp(InputManager.ACTION.SHOOT))
        {
            controller.SwitchState(controller.bowFiringState);
        }
    }
}
