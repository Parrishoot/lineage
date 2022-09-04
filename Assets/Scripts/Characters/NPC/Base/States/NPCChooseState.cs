using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChooseState<T> : NPCBaseState<T>
    where T : NPCStateController<T>
{
    public override void EnterState(T controller)
    {
        controller.ChooseNextState();
    }

    public override void ExitState(T controller)
    {
        
    }

    public override void FixedUpdateState(T controller)
    {
        
    }

    public override void UpdateState(T controller)
    {
        
    }
}
