using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerRecoilState : NPCBaseState<ChargerStateController>
{
    private float currentRecoilTime;

    public override void EnterState(ChargerStateController controller)
    {
        currentRecoilTime = controller.recoilTime;
    }

    public override void ExitState(ChargerStateController controller)
    {

    }

    public override void FixedUpdateState(ChargerStateController controller)
    {

    }

    public override void UpdateState(ChargerStateController controller)
    {
        currentRecoilTime -= Time.deltaTime;
        if(currentRecoilTime < 0)
        {
            controller.SwitchState(controller.chooseState);
        }
    }
}
