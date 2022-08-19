using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : PlayerBaseState
{
    public override void EnterState(PlayerStateController controller)
    {
        controller.rigidbody.velocity = Vector2.zero;
        CameraController.GetInstance().SetConversation(controller.gameObject, controller.GetCurrentInteractable().gameObject);
        controller.GetCurrentInteractable().Interact();

        controller.animator.SetInteger(PlayerStateController.ANIMATOR_MOVEMENT_PARAMETER_NAME, (int)PlayerStateController.PLAYER_STATE.INTERACTING);
    }

    public override void ExitState(PlayerStateController controller)
    {
        
    }

    public override void FixedUpdateState(PlayerStateController controller)
    {
        
    }

    public override void UpdateState(PlayerStateController controller)
    {
        if (controller.GetCurrentInteractable() == null || controller.GetCurrentInteractable().InteractionFinished())
        {
            controller.AttachCamera();
            controller.SwitchState(controller.playerIdleState);
        }
    }
}