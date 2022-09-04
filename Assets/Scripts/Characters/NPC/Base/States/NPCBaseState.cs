using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCBaseState<T> : IState<T>
    where T : NPCStateController<T>
{
    public abstract void EnterState(T controller);
    public abstract void ExitState(T controller);
    public abstract void FixedUpdateState(T controller);
    public abstract void UpdateState(T controller);
}
