using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationQuestNode", menuName = "Quests/Nodes/Conversation Quest Node", order = 1)]
public class ConversationQuestNodeMetadata : QuestNodeMetadata
{
    public string npcName;

    public ConversationQuestNodeMetadata() : base(QUEST_NODE_TYPE.CONVERSATION) { }
}
