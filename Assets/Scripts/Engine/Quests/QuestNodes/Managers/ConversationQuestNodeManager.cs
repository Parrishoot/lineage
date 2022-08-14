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

    public override void Activate()
    {
        npcController = NPCMasterManager.GetInstance().GetNPCController(metadata.npcType);

        npcController.AddQuestDialogue(metadata.dialogue, this);

        base.Activate();
    }

    public override string ToString()
    {
        return "Talk to " + metadata.npcType.ToString();
    }
}
