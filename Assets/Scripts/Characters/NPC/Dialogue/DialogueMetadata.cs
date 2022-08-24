using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 2)]
public class DialogueMetadata : ScriptableObject
{

    public List<DialogueNodeMetadata> dialoguePassages;

    [System.Serializable]
    public class DialogueNodeMetadata
    {
        public float charactersPerSecond = 20f;

        public string text = "Some text!";
    }
}
