using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestUIController : MonoBehaviour
{
    public TextMeshProUGUI questHeaderText;

    public GameObject questNodeUIFrame;

    public void SetHeaderText(string questName)
    {
        questHeaderText.text = questName;
    }

    public void AddQuestNode(GameObject questNodeUIGameObject)
    {
        questNodeUIGameObject.transform.SetParent(questNodeUIFrame.transform, false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
