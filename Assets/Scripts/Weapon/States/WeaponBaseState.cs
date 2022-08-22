using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBaseState<TController> : IState<TController>
    where TController: WeaponController<TController>
{
    public abstract void EnterState(TController controller);
    public abstract void ExitState(TController controller);
    public abstract void FixedUpdateState(TController controller);
    public abstract void UpdateState(TController controller);
}
