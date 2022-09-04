using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveState<T> : NPCBaseState<T>
    where T : NPCStateController<T>
{
    float flipTarget;
    Vector3 destination;

    public override void EnterState(T controller)
    {
        destination = controller.FindNewDestination();
    }

    public override void ExitState(T controller)
    {

    }

    public override void FixedUpdateState(T controller)
    {

    }

    public override void UpdateState(T controller)
    {
        if (!controller.GetNavMeshAgent().velocity.x.Equals(0))
        {
            float flipTarget = 180 - (Mathf.Sign(controller.GetNavMeshAgent().velocity.x) + 1) * 90;
            controller.GetFlipper().SetFlipTarget(flipTarget);
        }

        if (controller.GetNavMeshAgent().remainingDistance <= controller.GetNavMeshAgent().stoppingDistance)
        {
            controller.SwitchState(controller.chooseState);
        }
    }
}
