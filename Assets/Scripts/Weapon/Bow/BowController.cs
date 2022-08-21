using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : WeaponController<BowBaseState, BowController>
{
    public float timeToCharge = 2f;

    public BowIdleState bowIdleState = new BowIdleState();
    public BowChargingState bowChargingState= new BowChargingState();
    public BowFullyChargedState bowFullyChargedState= new BowFullyChargedState();
    public BowFiringState bowFiringState = new BowFiringState();

    public override bool IsAiming()
    {
        return false;
    }

    public override void Start()
    {
        currentState = bowIdleState;

        base.Start();
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
