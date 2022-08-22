using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<TStatemachine> : MonoBehaviour
    where TStatemachine: StateMachine<TStatemachine>
{
    protected IState<TStatemachine> currentState;

    // Start is called before the first frame update
    public virtual void Start()
    {
        SwitchState(currentState);        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        currentState.UpdateState((TStatemachine) this);
    }

    public virtual void FixedUpdate()
    {
        currentState.FixedUpdateState((TStatemachine) this);
    }

    public void SwitchState(IState<TStatemachine> newState)
    {
       if(currentState != null)
       {
            currentState.ExitState((TStatemachine) this);
       }
       else
       {
            Debug.Log("Current State is Null");
       }

       currentState = newState;
       currentState.EnterState((TStatemachine) this);
    }
}
