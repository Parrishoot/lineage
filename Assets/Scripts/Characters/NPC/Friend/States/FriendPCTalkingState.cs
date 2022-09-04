using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendPCTalkingState: NPCBaseState<FriendPCStateController>
{
    public override void EnterState(FriendPCStateController controller)
    {
        controller.GetNavMeshAgent().isStopped = true;

        float flipTarget = 180 - (Mathf.Sign(GameObject.FindGameObjectWithTag(PlayerMeta.PLAYER_TAG).gameObject.transform.position.x - controller.transform.position.x) + 1) * 90;
        controller.GetFlipper().SetFlipTarget(flipTarget);
        
    }

    public override void ExitState(FriendPCStateController controller)
    {
        
    }

    public override void FixedUpdateState(FriendPCStateController controller)
    {
        
    }

    public override void UpdateState(FriendPCStateController controller)
    {
        if(controller.GetDialogueInteractable().InteractionFinished())
        {
            controller.SwitchState(controller.chooseState);
        }
    }
}
