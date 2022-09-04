using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWaitState<T> : NPCBaseState<T>
    where T : NPCStateController<T>
{
    float waitTime = 0f;

    public override void EnterState(T controller)
    {
        waitTime = Random.Range(controller.minWaitTime, controller.maxWaitTime);
    }

    public override void ExitState(T controller)
    {
        
    }

    public override void FixedUpdateState(T controller)
    {
        
    }

    public override void UpdateState(T controller)
    {
        waitTime -= Time.deltaTime;

        if(waitTime < 0)
        {
            controller.SwitchState(controller.chooseState);
        }
    }
}
