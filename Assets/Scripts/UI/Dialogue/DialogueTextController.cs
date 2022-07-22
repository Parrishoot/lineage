using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTextController : MonoBehaviour
{

    public GameObject dialogueGuiContainer;

    public TextMeshProUGUI dialogueTextMesh;
    public Image dialogueTextBackground;
    public GameObject continueIndicator;

    public void SetText(string dialogueText)
    {
        dialogueTextMesh.text = dialogueText;
    }

    public void SetGUIEnabled(bool enabled)
    {
        dialogueGuiContainer.SetActive(enabled);
    }

    public void SetContinueIndicator(bool enabled)
    {
        continueIndicator.SetActive(enabled);
    }
}
