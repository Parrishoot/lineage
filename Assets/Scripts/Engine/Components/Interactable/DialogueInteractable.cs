using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : Interactable
{
    public List<DialogueMetadata> dialogueOptions;
    public AudioClip voice;

    private Queue<Dialogue> questDialogue = new Queue<Dialogue>();
    private List<Dialogue> randomDialogueOptions = new List<Dialogue>();
    private DialogueManager dialogueManager;

    public void AddQuestDialogue(DialogueMetadata questDialogueMetadata, ConversationQuestNodeManager questNodeManager)
    {
        questDialogue.Enqueue(new QuestDialogue(questDialogueMetadata, questNodeManager));
    }

    public override void Start()
    {
        base.Start();

        foreach (DialogueMetadata metadata in dialogueOptions)
        {
            randomDialogueOptions.Add(new Dialogue(metadata));
        }

        dialogueManager = DialogueManager.GetInstance();
    }

    public void Update()
    {
        if (InteractionInProgress() && dialogueManager.IsFinished())
        {
            interactionState = INTERACTION_STATE.INACTIVE;
        }
    }

    public override void Interact()
    {
        base.Interact();

        if (questDialogue.Count > 0)
        {
            dialogueManager.SetDialogue(questDialogue.Dequeue(), voice);
        }
        else
        {
            DialogueManager.GetInstance().SetDialogue(randomDialogueOptions[Random.Range(0, randomDialogueOptions.Count)], voice);
        }
    }
}
