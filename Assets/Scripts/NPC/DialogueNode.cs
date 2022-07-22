using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string dialogue;
    public float charactersPerSecond = 10f;

    public DialogueNode(string dialogue, float charactersPerSecond = 10f)
    {
        this.dialogue = dialogue;
        this.charactersPerSecond = charactersPerSecond;
    }

    public float GetTickerTime()
    {
        return 1 / charactersPerSecond;
    }

}