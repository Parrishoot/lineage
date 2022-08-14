using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestNodeManager
{
    public enum QUEST_NODE_STATE
    {
        INACTIVE,
        ACTIVE,
        FINISHED
    }

    private QUEST_NODE_STATE questNodeState = QUEST_NODE_STATE.INACTIVE;

    private QuestNodeUIController questNodeUIController;

    public void SetQuestNodeUIController(QuestNodeUIController newQuestNodeUIController)
    {
        // When setting the UI Controller, set the text to be the node text
        questNodeUIController = newQuestNodeUIController;
        questNodeUIController.SetText(ToString());
    }

    public QuestNodeUIController GetQuestNodeUIController()
    {
        return questNodeUIController;
    }

    public string GetText()
    {
        return ToString();
    }

    public void Begin()
    {

    }

    public void Complete()
    {
        questNodeUIController.SetComplete();
        questNodeState = QUEST_NODE_STATE.FINISHED;
    }

    public virtual void Activate()
    {
        questNodeUIController.SetInProgress();
        questNodeState = QUEST_NODE_STATE.ACTIVE;
    }

    public bool IsFinished()
    {
        return questNodeState == QUEST_NODE_STATE.FINISHED;
    }

    public bool IsActive()
    {
        return questNodeState == QUEST_NODE_STATE.ACTIVE;
    }

    public bool IsInactive()
    {
        return questNodeState == QUEST_NODE_STATE.INACTIVE;
    }

    public abstract override string ToString();
}
