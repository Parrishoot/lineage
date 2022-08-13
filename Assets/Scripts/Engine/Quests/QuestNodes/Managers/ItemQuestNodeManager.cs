using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuestNodeManager : QuestNodeManager
{

    private ItemQuestNodeMetadata metadata;

    public ItemQuestNodeManager(ItemQuestNodeMetadata metadata, QuestNodeUIController questNodeUIController)
    {
        this.metadata = metadata;
        SetQuestNodeUIController(questNodeUIController);
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
