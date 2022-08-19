using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGenericUpdateBaseState<T>
{
    void EnterState(T controller);

    void UpdateState(T controller);

    void ExitState(T controller);
}
