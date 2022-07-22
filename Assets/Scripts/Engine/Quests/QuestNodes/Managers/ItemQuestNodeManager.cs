using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuestNodeManager : QuestNodeManager
{

    private ItemQuestNodeMetadata metadata;

    public ItemQuestNodeManager(ItemQuestNodeMetadata metadata)
    {
        this.metadata = metadata;
    }

    public override bool IsFinished()
    {
        throw new System.NotImplementedException();
    }

    public override string ToString()
    {
        return "Find the " + metadata.itemName;
    }
}
