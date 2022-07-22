using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestTextController : MonoBehaviour
{
    public TextMeshProUGUI questNameTextMesh;
    public TextMeshProUGUI questNodeTextMesh;

    public void SetText(string questNameText, string questNodeText) {

        questNameTextMesh.text = questNameText;
        questNodeTextMesh.text = questNodeText;

    }
}
