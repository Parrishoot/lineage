using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : Interactable
{
    public AudioClip voice;
    public NPCMasterManager.NPCType npcType = NPCMasterManager.NPCType.NOBODY;
    public List<DialogueMetadata> dialogueOptions;
    public float wanderRadius = 0f;
    [Range(0, 1)]
    public float wanderChance = .5f;
    [Range(0, 2)]
    public float maxWaitTime = 1f;

    public enum NPC_STATE
    {
        INTERACTING,
        WANDERING,
        IDLE,
        WAITING
    }

    private NPC_STATE npcState = NPC_STATE.WAITING;

    private Queue<Dialogue> questDialogue = new Queue<Dialogue>();
    private List<Dialogue> randomDialogueOptions = new List<Dialogue>();
    private DialogueManager dialogueManager;
    private NavMeshAgent navMeshAgent;
    private Vector3 startingPosition;
    private float currentWaitTime;
    private Vector3 destination;
    private float flipTarget;

    public void Awake()
    {
        base.Start();

        foreach (DialogueMetadata metadata in dialogueOptions)
        {
            randomDialogueOptions.Add(new Dialogue(metadata));
        }

        dialogueManager = DialogueManager.GetInstance();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        startingPosition = transform.position;
    }

    public NPCMasterManager.NPCType GetNPCType()
    {
        return npcType;
    }

    public void Update()
    {

        switch (npcState)
        {
            case NPC_STATE.INTERACTING:

                if (InteractionInProgress() && dialogueManager.IsFinished())
                {
                    interactionState = INTERACTION_STATE.INACTIVE;
                    npcState = NPC_STATE.IDLE;
                }

                break;

            case NPC_STATE.IDLE:

                float wanderRoll = Random.Range(0f, 1f);

                if (!wanderRadius.Equals(0f) && wanderRoll < wanderChance)
                {

                    destination = startingPosition + (Vector3)(wanderRadius * Random.insideUnitCircle);

                    navMeshAgent.SetDestination(destination);
                    navMeshAgent.isStopped = false;

                    npcState = NPC_STATE.WANDERING;
                }
                else
                {
                    currentWaitTime = Random.Range(0, maxWaitTime);
                    npcState = NPC_STATE.WAITING;
                }

                break;

            case NPC_STATE.WAITING:

                currentWaitTime -= Time.deltaTime;

                if (currentWaitTime <= 0f)
                {
                    npcState = NPC_STATE.IDLE;
                }

                break;

            case NPC_STATE.WANDERING:

                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    npcState = NPC_STATE.IDLE;
                }

                break;

        }

        if (InteractionInProgress())
        {
            npcState = NPC_STATE.INTERACTING;
        }

        

        if(!navMeshAgent.velocity.x.Equals(0))
        {
            flipTarget = 180 - (Mathf.Sign(navMeshAgent.velocity.x) + 1) * 90;
        }
        SetFlip(flipTarget, Time.deltaTime);
    }

    public void AddQuestDialogue(DialogueMetadata questDialogueMetadata, ConversationQuestNodeManager questNodeManager)
    {
        questDialogue.Enqueue(new QuestDialogue(questDialogueMetadata, questNodeManager));
    }

    public override void Interact()
    {
        interactionState = Interactable.INTERACTION_STATE.IN_PROGRESS;
        npcState = NPC_STATE.INTERACTING;

        navMeshAgent.isStopped = true;

        // Look at the player as they're talking to you
        flipTarget = 180 - (Mathf.Sign(GameObject.FindGameObjectWithTag(PlayerMeta.PLAYER_TAG).gameObject.transform.position.x - transform.position.x) + 1) * 90;

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
