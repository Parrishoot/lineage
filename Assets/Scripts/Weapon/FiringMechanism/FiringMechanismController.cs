using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringMechanismController : MonoBehaviour
{

    public float yOffset = 1f;

    private Transform originalParent;

    public virtual void Start()
    {
        originalParent = gameObject.transform.parent;
        gameObject.transform.parent = null;
    }

    public virtual void Update()
    {
        gameObject.transform.position = originalParent.position + Vector3.up * yOffset;
    }

    protected enum FIRING_MECHANISM_STATES
    {
        IDLE,
        PENDING,
        POWER_SHOT,
        REGULAR_SHOT,
        FAILURE,
        RESETTING
    }

    protected FIRING_MECHANISM_STATES firingMechanismState = FIRING_MECHANISM_STATES.IDLE;

    public bool isRegularShot()
    {
        return firingMechanismState.Equals(FIRING_MECHANISM_STATES.REGULAR_SHOT);
    }

    public bool isPowerShot()
    {
        return firingMechanismState.Equals(FIRING_MECHANISM_STATES.POWER_SHOT);
    }

    public bool isFailure()
    {
        return firingMechanismState.Equals(FIRING_MECHANISM_STATES.FAILURE);
    }

    public bool isPending()
    {
        return firingMechanismState.Equals(FIRING_MECHANISM_STATES.PENDING);
    }

    public bool isResetting()
    {
        return firingMechanismState.Equals(FIRING_MECHANISM_STATES.RESETTING);
    }

    public bool isIdle()
    {
        return firingMechanismState.Equals(FIRING_MECHANISM_STATES.IDLE);
    }
    public void Reset()
    {
        firingMechanismState = FIRING_MECHANISM_STATES.RESETTING;
    }

    public void SetIdle()
    {
        firingMechanismState = FIRING_MECHANISM_STATES.IDLE;
    }
}
