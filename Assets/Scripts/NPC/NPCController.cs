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

        foreach(DialogueMetadata metadata in dialogueOptions)
        {
            randomDialogueOptions.Add(new Dialogue(metadata));
        }

        dialogueManager = DialogueManager.GetInstance();
    }

    public NPCMasterManager.NPCType GetNPCType()
    {
        return npcType;
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

        // Look at the player as they're talking to you

        Vector3 newScale = new Vector3(Mathf.Sign(GameObject.FindGameObjectWithTag(PlayerStateController.PLAYER_TAG).gameObject.transform.position.x - transform.position.x),
                                       transform.localScale.y,
                                       transform.localScale.z);

        Debug.Log(newScale);

        transform.localScale = newScale;

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
