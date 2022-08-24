using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestDialogue : Dialogue
{
    private QuestNodeManager questNodeManager;

    public QuestDialogue(DialogueMetadata metadata, ConversationQuestNodeManager newQuestNodeManager) : base(metadata) {
        questNodeManager = newQuestNodeManager;
    }

    public override void EndDialogue()
    {
        base.EndDialogue();

        questNodeManager.Complete();
    }

}
