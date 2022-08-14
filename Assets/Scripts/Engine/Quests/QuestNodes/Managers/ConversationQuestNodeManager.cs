using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationQuestNodeManager : QuestNodeManager
{

    private ConversationQuestNodeMetadata metadata;

    private NPCController npcController;

    public ConversationQuestNodeManager(ConversationQuestNodeMetadata metadata, QuestNodeUIController questNodeUIController)
    {
        this.metadata = metadata;
        SetQuestNodeUIController(questNodeUIController);
    }

    public void AddQuestDialogue()
    {
        npcController = NPCMasterManager.GetInstance().GetNPCController(metadata.npcType);

        if(npcController != null)
        {
            npcController.AddQuestDialogue(metadata.dialogue, this);
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
