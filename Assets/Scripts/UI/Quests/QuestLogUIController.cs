using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogUIController : MonoBehaviour
{
    public void AddQuestUIElement(GameObject questUIElement)
    {
        questUIElement.transform.SetParent(transform, false);
    }
}
