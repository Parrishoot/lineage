using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageState<T> : EnemyBaseState<T>
    where T : EnemyStateController<T>
{
    public override void EnterState(T controller)
    {
        
    }

    public override void ExitState(T controller)
    {
        
    }

    public override void FixedUpdateState(T controller)
    {
        
    }

    public override void UpdateState(T controller)
    {
        if(!controller.healthController.DamageIndicatorActive())
        {
            controller.SwitchState(controller.chooseState);
        }
    }
}
