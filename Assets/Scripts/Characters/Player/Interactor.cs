using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private Interactable interactable = null;

    public void AddInteractable(Interactable interactable)
    // TODO: Change this to be a trigger on the player instead of the 
    // objects such that we can queue up interactable objects and set an
    // active one rather than resetting this potentially in the middle 
    // of an interaction
    {
        this.interactable = interactable;
    }
    public Interactable GetCurrentInteractable()
    {
        return interactable;
    }

    public void RemoveInteractable(Interactable interactable)
    {
        if (this.interactable.Equals(interactable))
        {
            this.interactable = null;
        }
    }

}
