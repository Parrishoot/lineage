using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestNodeUIController : MonoBehaviour
{

    public TextMeshProUGUI questNodeText;

    public Color inProgressBackgroundColor;
    public Color inProgressTextColor;

    public Color completeBackgroundColor;
    public Color completeTextColor;

    public void SetText(string questNodeText)
    {

        this.questNodeText.text = questNodeText;

    }

    public void SetInProgress()
    {
        GetComponent<Image>().color = inProgressBackgroundColor;
        questNodeText.color = inProgressTextColor;
    }

    public void SetComplete()
    {
        GetComponent<Image>().color = completeBackgroundColor;
        
        questNodeText.fontStyle = FontStyles.Strikethrough;
        questNodeText.color = completeTextColor;
    }
}
