using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerIndicatorState : NPCBaseState<ChargerStateController>
{

    private float currentIndicatorTime;

    public override void EnterState(ChargerStateController controller)
    {
        
    }

    public override void ExitState(ChargerStateController controller)
    {
        
    }

    public override void FixedUpdateState(ChargerStateController controller)
    {
        
    }

    public override void UpdateState(ChargerStateController controller)
    {
        currentIndicatorTime -= Time.deltaTime;

        if(currentIndicatorTime <= 0)
        {
            controller.SwitchState(controller.chargerChargeState);
        }
    }
}
