using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationQuestNodeManager : QuestNodeManager
{

    private ConversationQuestNodeMetadata metadata;

    public ConversationQuestNodeManager(ConversationQuestNodeMetadata metadata)
    {
        this.metadata = metadata;
    }

    public override bool IsFinished()
    {
        throw new System.NotImplementedException();
    }

    public override string ToString()
    {
        return "Talk to " + metadata.npcName;
    }
}
