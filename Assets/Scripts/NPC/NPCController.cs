using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : Interactable
{
    public AudioClip voice;

    public NPCMasterManager.NPCType npcType = NPCMasterManager.NPCType.NOBODY;

    public List<DialogueMetadata> dialogueOptions;

    private Queue<Dialogue> questDialogue = new Queue<Dialogue>();

    private List<Dialogue> randomDialogueOptions = new List<Dialogue> ();

    private DialogueManager dialogueManager;

    public void Awake()
    {
        base.Start();

        // If the NPC isn't a nobody, add it to the
        if(npcType != NPCMasterManager.NPCType.NOBODY)
        {
            NPCMasterManager.GetInstance().AddNPCController(npcType, this);
        }

        foreach(DialogueMetadata metadata in dialogueOptions)
        {
            randomDialogueOptions.Add(new Dialogue(metadata));
        }

        dialogueManager = DialogueManager.GetInstance();
    }

    public void Update()
    {
        if(InteractionInProgress() && dialogueManager.IsFinished())
        {
            interactionState = INTERACTION_STATE.INACTIVE;
        }
    }

    public void AddQuestDialogue(DialogueMetadata questDialogueMetadata, ConversationQuestNodeManager questNodeManager)
    {
        questDialogue.Enqueue(new QuestDialogue(questDialogueMetadata, questNodeManager));
    }

    public override void Interact()
    {
        interactionState = Interactable.INTERACTION_STATE.IN_PROGRESS;

        if(questDialogue.Count > 0)
        {
            dialogueManager.SetDialogue(questDialogue.Dequeue(), voice);
        }
        else
        {
            DialogueManager.GetInstance().SetDialogue(randomDialogueOptions[Random.Range(0, randomDialogueOptions.Count)], voice);
        }
    }
}
