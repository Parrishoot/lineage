using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerStateController : EnemyStateController<ChargerStateController>
{
    public LayerMask chargeStopLayers;

    public float chargeSpeed = 10f;

    public float recoilTime = 1f;

    public float indicatorTime = 1;

    public Transform raycastOriginPoint;

    [Range(0, 10)]
    public float chargeChance;

    public ChargerIndicatorState chargerIndicatorState = new ChargerIndicatorState();
    public ChargerRecoilState chargerRecoilState = new ChargerRecoilState();
    public ChargerChargeState chargerChargeState = new ChargerChargeState();

    public override float GetRandomRoll()
    {
        return Random.Range(0, wanderChance + waitChance + recoilTime);
    }
    
    public override void ProcessSpecificBehavior(float randomRoll)
    {
        SwitchState(chargerIndicatorState);
    }
}
