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
    
    [Range(0, 10)]
    public float wanderChance = 5f;

    [Range(0, 10)]
    public float waitChance = 5f;

    [Range(0, 10)]
    public float maxWanderTime = 0f;
    
    [Range(0, 2)]
    public float maxWaitTime = 1f;

    [Range(.25f, .5f)]
    public float minWaitTime = .3f;

    [Range(0, 10)]
    public float moveSpeed = 1f;

    private NavMeshAgent navMeshAgent;
    private NPCFlipper flipper;
    private Vector3 startingPosition;

    public NPCChooseState<T> chooseState = new NPCChooseState<T>();
    public NPCMoveState<T> moveState = new NPCMoveState<T>();
    public NPCWaitState<T> waitState = new NPCWaitState<T>();

    public virtual float GetRandomRoll()
    {
        return Random.Range(0f, wanderChance + waitChance);
    }

    public virtual void ProcessSpecificBehavior(float randomRoll)
    {
        SwitchState(waitState);
    }

    public virtual void ChooseNextState()
    {
        float randomRoll = GetRandomRoll();

        if (randomRoll <= wanderChance)
        {
            SwitchState(moveState);
        }
        else if(randomRoll <= wanderChance + waitChance)
        {
            SwitchState(waitState);
        }
        else
        {
            ProcessSpecificBehavior(randomRoll);
        }
    }

    public Vector3 GetRandomDestinationInRadius()
    {
        return startingPosition + (Vector3)(wanderRadius * Random.insideUnitCircle);
    }

    public Vector3 SetRandomWander(bool beginMove=false)
    {
        Vector3 destination = GetRandomDestinationInRadius();

        navMeshAgent.SetDestination(destination);
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = moveSpeed;

        if (beginMove)
        {
            SwitchState(moveState);
        }

        return destination;
    }

    public virtual Vector3 FindNewDestination(bool beginMove=false)
    {
        return SetRandomWander(beginMove);
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
