using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestNode", menuName = "Quests/Nodes/Default Quest Node", order = 1)]
public class QuestNodeMetadata: ScriptableObject
{
    public enum QUEST_NODE_TYPE
    {
        CONVERSATION,
        KILL,
        ITEM,
        TRIGGER,
        CONTAINER
    }

    public QuestNodeMetadata(QUEST_NODE_TYPE questNodeType)
    {
        this.questNodeType = questNodeType;
    }

    // The type of quest node that is active
    public QUEST_NODE_TYPE questNodeType;

    // If this is a CONTAINER quest node type,
    // the container holds all of the quest nodes that 
    // need to be completed in order to move on to the 
    // next node of the quest. I had to make a new 
    // class to get rid of the warning which is dumb
    // public List<QuestNodeMetaData> containerQuestNodes = null;

}
