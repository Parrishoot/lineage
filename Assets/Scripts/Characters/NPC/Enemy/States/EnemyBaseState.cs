using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState<T> : NPCBaseState<T>
    where T: EnemyStateController<T>
{

}
