using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // TODO: Add more here idk
    // Ideally I'd like to add a system that could do cool text effects
    // And auto-wrap statements
    // I'd also love this to be a LinkedList but its not serializable and
    // I'm too lazy to make another custom editor right now
    public List<DialogueNode> dialoguePassages;

    private int currentIndex = 0;

    public string BeginDialogue()
    {
        currentIndex = 0;
        return dialoguePassages[currentIndex].dialogue;
    }

    // Returns the next text in the dialogue passages
    // if its at the end, will return null
    public string GetNextText(bool moveForward = false)
    {
        if (moveForward)
        {
            currentIndex++;
            return GetCurrentText();
        }
        else
        {
            if (currentIndex >= dialoguePassages.Count)
            {
                return null;
            }
            else
            {
                return dialoguePassages[currentIndex - 1].dialogue;
            }
        }
    }

    // Returns the previous text in the dialogue passages
    // if it's t the end, will return null
    public string GetPreviousText(bool moveBackward = false)
    {
        if (moveBackward)
        {
            currentIndex--;
            return GetCurrentText();
        }
        else
        {
            if(currentIndex <= 0)
            {
                return null;
            }
            else
            {
                return dialoguePassages[currentIndex - 1].dialogue;
            }
        }
        
    }

    // Returns the current text in the dialogue passage
    public string GetCurrentText()
    {
        if (currentIndex >= dialoguePassages.Count)
        {
            return null;
        }

        return dialoguePassages[currentIndex].dialogue;
    }

    public bool IsFinished()
    {
        return currentIndex >= dialoguePassages.Count;
    }

    public float GetCurrentTickerTime()
    {
        return dialoguePassages[currentIndex].GetTickerTime();
    }
}


