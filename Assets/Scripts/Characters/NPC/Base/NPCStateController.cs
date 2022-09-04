using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NPCFlipper))]
public abstract class NPCStateController<T> : StateMachine<T>, INPC
    where T : NPCStateController<T>
{
    public float wanderRadius = 0f;
    
    [Range(0, 1)]
    public float wanderChance = .5f;
    
    [Range(0, 2)]
    public float maxWaitTime = 1f;

    [Range(.25f, .5f)]
    public float minWaitTime = .3f;

    private NavMeshAgent navMeshAgent;
    private NPCFlipper flipper;
    private Vector3 startingPosition;

    public NPCChooseState<T> chooseState = new NPCChooseState<T>();
    public NPCMoveState<T> moveState = new NPCMoveState<T>();
    public NPCWaitState<T> waitState = new NPCWaitState<T>();

    public virtual void ChooseNextState()
    {
        float randomRoll = Random.Range(0f, 1f);

        if (randomRoll < wanderChance)
        {
            SwitchState(moveState);
        }
        else
        {
            SwitchState(waitState);
        }
    }

    public Vector3 FindNewDestination(bool beginMove=false)
    {
        Vector3 destination = startingPosition + (Vector3)(wanderRadius * Random.insideUnitCircle);

        navMeshAgent.SetDestination(destination);
        navMeshAgent.isStopped = false;

        if(beginMove)
        {
            SwitchState(moveState);
        }

        return destination;
    }

    public override void Start()
    {
        flipper = GetComponent<NPCFlipper>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        currentState = chooseState;

        base.Start();
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return navMeshAgent;
    }

    public NPCFlipper GetFlipper()
    {
        return flipper;
    }
}
