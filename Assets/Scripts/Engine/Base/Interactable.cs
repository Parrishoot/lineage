using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class Interactable : MonoBehaviour
{

    protected enum INTERACTION_STATE
    {
        INACTIVE,
        IN_PROGRESS
    }

    protected INTERACTION_STATE interactionState = INTERACTION_STATE.INACTIVE;

    CircleCollider2D interactableRadius;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // Just makes sure that the radius is set as a trigger regardless of
        // How it's configured in the inspector
        interactableRadius = GetComponent<CircleCollider2D>();
        interactableRadius.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStateController>().AddInteractable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStateController>().RemoveInteractable(this);
        }
    }

    public void FinishInteraction()
    {
        interactionState = INTERACTION_STATE.INACTIVE;
    }

    public bool InteractionFinished()
    {
        return interactionState == INTERACTION_STATE.INACTIVE;
    }

    public bool InteractionInProgress()
    {
        return interactionState == INTERACTION_STATE.IN_PROGRESS;
    }

    public abstract void Interact();

}
