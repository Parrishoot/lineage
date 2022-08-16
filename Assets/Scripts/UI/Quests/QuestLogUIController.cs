using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogUIController : MonoBehaviour
{

    public Button saveButton;
    public Button loadButton;

    public void Start()
    {
        saveButton.onClick.AddListener(Save);
        loadButton.onClick.AddListener(Load);

    }

    public void Save()
    {
        QuestLogManager.GetInstance().SaveActiveQuest();
    }

    public void Load()
    {
        QuestLogManager.GetInstance().LoadActiveQuest();
    }



    public void AddQuestUIElement(GameObject questUIElement)
    {
        questUIElement.transform.SetParent(transform, false);
    }
}
