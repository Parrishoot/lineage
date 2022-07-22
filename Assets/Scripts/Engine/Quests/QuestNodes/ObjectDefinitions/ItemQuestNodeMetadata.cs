using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemQuestNode", menuName = "Quests/Nodes/Item Quest Node", order = 1)]
public class ItemQuestNodeMetadata : QuestNodeMetadata
{
    public string itemName = "default";

    public ItemQuestNodeMetadata() : base(QUEST_NODE_TYPE.ITEM) { }
}
