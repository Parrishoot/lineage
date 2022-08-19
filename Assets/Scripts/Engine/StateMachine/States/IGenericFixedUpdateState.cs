using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGenericFixedUpdateBaseState<T> : IGenericUpdateBaseState<T>
{
    void FixedUpdateState(T controller);
}
