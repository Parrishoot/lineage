using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBaseState<TState, TController> : IState<TState, TController>
    where TState: WeaponBaseState<TState, TController>
    where TController: WeaponController<TState, TController>
{
    public abstract void EnterState(TController controller);
    public abstract void ExitState(TController controller);
    public abstract void FixedUpdateState(TController controller);
    public abstract void UpdateState(TController controller);
}
