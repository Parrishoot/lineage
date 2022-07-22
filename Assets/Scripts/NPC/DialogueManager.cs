using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{

    public DialogueTextController dialogueTextController;

    private enum DIALOGUE_STATE
    {
        INACTIVE,
        ACTIVE,
        WAITING
    }

    private DIALOGUE_STATE dialogueState = DIALOGUE_STATE.INACTIVE;
    
    // Variables to keep track of current dialogue
    private string currentDialogueString = "";
    private int currentDialogueCharacterIndex = 0;
    private float currentTickerTime = 0f;

    // The active piece of dialogue
    private Dialogue currentDialogue = null;
   
    public void SetDialogue(Dialogue dialogue)
    {
        dialogue.BeginDialogue();
        currentDialogue = dialogue;
        dialogueTextController.SetGUIEnabled(true);
        dialogueState = DIALOGUE_STATE.ACTIVE;

        ResetDialogueVariables();
    }

    private void ResetDialogueVariables()
    {
        currentDialogueString = "";
        currentDialogueCharacterIndex = 0;
        currentTickerTime = 0f;
    }

    public bool IsFinished()
    {
        return dialogueState == DIALOGUE_STATE.INACTIVE;
    }

    public void Update()
    {
        if(dialogueState == DIALOGUE_STATE.ACTIVE)
        {
            currentTickerTime += Time.deltaTime;

            if(currentTickerTime >= currentDialogue.GetCurrentTickerTime())
            {
                currentDialogueString += currentDialogue.GetCurrentText()[currentDialogueCharacterIndex++];
                currentTickerTime = 0f;

                if(currentDialogue.GetCurrentText().Equals(currentDialogueString))
                {
                    dialogueState = DIALOGUE_STATE.WAITING;
                    dialogueTextController.SetContinueIndicator(true);
                }
            }

            if (InputManager.GetInstance().GetKeyDown(InputManager.ACTION.INTERACT))
            {
                currentDialogueString = currentDialogue.GetCurrentText();
                dialogueState = DIALOGUE_STATE.WAITING;
                dialogueTextController.SetContinueIndicator(true);
            }

            dialogueTextController.SetText(currentDialogueString);
        }
        else if(dialogueState == DIALOGUE_STATE.WAITING)
        {
            if (InputManager.GetInstance().GetKeyDown(InputManager.ACTION.INTERACT))
            {
                string nextString = currentDialogue.GetNextText(true);
                if(nextString == null)
                {
                    dialogueTextController.SetGUIEnabled(false);
                    currentDialogue = null;
                    dialogueState = DIALOGUE_STATE.INACTIVE;
                }
                else
                {
                    ResetDialogueVariables();
                    dialogueState = DIALOGUE_STATE.ACTIVE;
                    dialogueTextController.SetContinueIndicator(false);
                }
            }
        }
    }
}
