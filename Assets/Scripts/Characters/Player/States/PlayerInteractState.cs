using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState<TStateMachine> : PlayerBaseState<TStateMachine>
    where TStateMachine : PlayerStateController<TStateMachine>
{
    public override void EnterState(TStateMachine controller)
    {
        controller.GetMover().rigidbody.velocity = Vector2.zero;
        CameraController.GetInstance().SetConversation(controller.gameObject, controller.GetInteractor().GetCurrentInteractable().gameObject);
        controller.GetInteractor().GetCurrentInteractable().Interact();

        controller.animator.SetInteger(PlayerMeta.ANIMATOR_MOVEMENT_PARAMETER_NAME, (int) PlayerStateController<TStateMachine>.PLAYER_STATE.INTERACTING);
    }

    public override void ExitState(TStateMachine controller)
    {
        
    }

    public override void FixedUpdateState(TStateMachine controller)
    {
        
    }

    public override void UpdateState(TStateMachine controller)
    {
        if (controller.GetInteractor().GetCurrentInteractable() == null || controller.GetInteractor().GetCurrentInteractable().InteractionFinished())
        {
            controller.AttachCamera();
            controller.SwitchState(controller.playerIdleState);
        }
    }
}
