using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationQuestNodeManager : QuestNodeManager
{

    private ConversationQuestNodeMetadata metadata;

    private DialogueInteractable dialogueInteractable;

    public ConversationQuestNodeManager(ConversationQuestNodeMetadata metadata, QuestNodeUIController questNodeUIController)
    {
        this.metadata = metadata;
        SetQuestNodeUIController(questNodeUIController);
    }

    public void AddQuestDialogue()
    {
        dialogueInteractable = NPCMasterManager.GetInstance().GetNPCController(metadata.npcType);

        if(dialogueInteractable != null)
        {
            dialogueInteractable.AddQuestDialogue(metadata.dialogue, this);
        }
    }

    public override void Activate()
    {
        AddQuestDialogue();

        base.Activate();
    }

    public override string ToString()
    {
        return "Talk to " + metadata.npcType.ToString();
    }

    public override void InitQuestOnSceneTransition()
    {
        AddQuestDialogue();
    }
}
