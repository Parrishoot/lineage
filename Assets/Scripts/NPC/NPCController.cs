using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : Interactable
{


    private Queue<Dialogue> questDialogue;

    public List<Dialogue> randomDialogueOptions;

    private DialogueManager dialogueManager;

    public override void Start()
    {
        base.Start();

        dialogueManager = DialogueManager.GetInstance();
    }

    public void Update()
    {
        if(InteractionInProgress() && dialogueManager.IsFinished())
        {
            interactionState = INTERACTION_STATE.INACTIVE;
        }
    }

    public override void Interact()
    {
        interactionState = Interactable.INTERACTION_STATE.IN_PROGRESS;
        DialogueManager.GetInstance().SetDialogue(randomDialogueOptions[0]);
    }
}
