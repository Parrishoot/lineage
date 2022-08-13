using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestNodeManager
{
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

    public abstract bool IsFinished();

    public abstract override string ToString();
}
