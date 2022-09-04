using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendPCStateController : NPCStateController<FriendPCStateController>
{
    public NPCMasterManager.NPCType npcType;

    public DialogueInteractable interactable;

    public FriendPCTalkingState talkingState = new FriendPCTalkingState();

    public DialogueInteractable GetDialogueInteractable()
    {
        return interactable;
    }

    public override void Update()
    {
        base.Update();

        CheckInteract();
    }

    private void CheckInteract()
    {
        if(interactable.InteractionInProgress())
        {
            SwitchState(talkingState);
        }
    }
}
