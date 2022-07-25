using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text;
using System.Text.RegularExpressions;

public class DialogueManager : Singleton<DialogueManager>
{

    public DialogueTextController dialogueTextController;

    public AudioSource audioSouce;


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
   
    public void SetDialogue(Dialogue dialogue, AudioClip voice)
    {
        dialogue.BeginDialogue();
        currentDialogue = dialogue;
        dialogueTextController.SetGUIEnabled(true);
        dialogueState = DIALOGUE_STATE.ACTIVE;

        audioSouce.clip = voice;

        ResetDialogueVariables(currentDialogue.GetCurrentText().Length);
    }

    private void ResetDialogueVariables(int stringLength = 0)
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

        // TODO: Clean this up its giving me anxiety

        if(dialogueState == DIALOGUE_STATE.ACTIVE)
        {
            currentTickerTime += Time.deltaTime;

            if(currentTickerTime >= currentDialogue.GetCurrentTickerTime())
            {
                if(!audioSouce.isPlaying)
                {
                    audioSouce.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                    audioSouce.Play();
                }
                currentDialogueString += currentDialogue.GetCurrentText()[currentDialogueCharacterIndex++];
                currentTickerTime = 0f;

                if(currentDialogue.GetCurrentText().Equals(currentDialogueString.ToString()))
                {
                    dialogueState = DIALOGUE_STATE.WAITING;
                    dialogueTextController.SetContinueIndicator(true);
                }
            }

            if (InputManager.GetInstance().GetKeyDown(InputManager.ACTION.INTERACT) && !Regex.IsMatch(currentDialogueString.ToString(), @"^$", RegexOptions.None))
            {
                currentDialogueString = currentDialogue.GetCurrentText();
                dialogueState = DIALOGUE_STATE.WAITING;
                dialogueTextController.SetContinueIndicator(true);
            }

            dialogueTextController.SetText(currentDialogueString.ToString());
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
                    ResetDialogueVariables(currentDialogue.GetCurrentText().Length);
                    dialogueState = DIALOGUE_STATE.ACTIVE;
                    dialogueTextController.SetContinueIndicator(false);
                }
            }
        }
    }
}
