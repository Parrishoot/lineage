using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerChargeState : NPCBaseState<ChargerStateController>
{
    private float chargeSpeed;
    private Vector3 chargeDirection;

    public override void EnterState(ChargerStateController controller)
    {
        chargeSpeed = controller.chargeSpeed;
        chargeDirection = controller.GetDirectonToPlayer();
        
    }

    public override void ExitState(ChargerStateController controller)
    {

    }

    public override void FixedUpdateState(ChargerStateController controller)
    {

    }

    public override void UpdateState(ChargerStateController controller)
    {
        // Debug.DrawLine(controller.raycastOriginPoint.position, controller.raycastOriginPoint.position + (chargeDirection * chargeSpeed * Time.deltaTime * 10), Color.red);
        if (Physics2D.Raycast(controller.raycastOriginPoint.position, chargeDirection, chargeSpeed * Time.deltaTime * 20, controller.chargeStopLayers))
        {
            controller.SwitchState(controller.chargerRecoilState);
        }

        controller.gameObject.transform.position += (chargeDirection * Time.deltaTime * chargeSpeed);
    }
}
