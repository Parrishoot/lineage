using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestNodeUIController : MonoBehaviour
{
    public TextMeshProUGUI questNodeText;

    public void SetText(string questNodeText)
    {

        this.questNodeText.text = questNodeText;

    }
}
