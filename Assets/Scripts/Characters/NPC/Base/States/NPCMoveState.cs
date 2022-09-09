using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveState<T> : NPCBaseState<T>
    where T : NPCStateController<T>
{
    private bool infiniteWander = true;
    private float wanderTime = 0f;

    protected virtual bool CheckForWalkFinishCondition(T controller)
    {
        return (controller.GetNavMeshAgent().remainingDistance <= controller.GetNavMeshAgent().stoppingDistance) || wanderTime <= 0f;
    }

    public override void EnterState(T controller)
    {
        if (controller.maxWanderTime != 0f)
        {
            infiniteWander = false;
            wanderTime = 0f;
        }
        else
        {
            wanderTime = 1f;
        }

        controller.FindNewDestination();
    }

    public override void ExitState(T controller)
    {

    }

    public override void FixedUpdateState(T controller)
    {

    }

    public override void UpdateState(T controller)
    {

        if(!infiniteWander)
        {
            wanderTime -= Time.deltaTime;
        }

        if (!controller.GetNavMeshAgent().velocity.x.Equals(0))
        {
            float flipTarget = 180 - (Mathf.Sign(controller.GetNavMeshAgent().velocity.x) + 1) * 90;
            controller.GetFlipper().SetFlipTarget(flipTarget);
        }

        if (CheckForWalkFinishCondition(controller))
        {
            controller.SwitchState(controller.chooseState);
        }
    }
}
