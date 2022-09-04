using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowController : PlayerWeaponStateController<PlayerBowController>
{
    public static string ANIMATION_STATE_NAME = "bowState";

    public float timeToCharge = 2f;

    public BowIdleState bowIdleState = new BowIdleState();
    public BowChargingState bowChargingState= new BowChargingState();
    public BowFullyChargedState bowFullyChargedState= new BowFullyChargedState();
    public BowFiringState bowFiringState = new BowFiringState();

    public override bool IsAiming()
    {
        return currentState != bowIdleState;
    }

    public override void Start()
    {
        currentState = bowIdleState;

        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public override void Reset()
    {
        SwitchState(bowIdleState);
    }
}
