using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Look into making this serializable for use in the Unity editor
[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest", order = 1)]
public class QuestMetadata: ScriptableObject
{
    [HideInInspector]
    // List of steps that a player must take to complete a quest
    public QuestNodeMetadata[] questNodes;

    public string questName;

    public string questDescription;

    public List<Timeline.BRANCHES> accessibleBranches = new List<Timeline.BRANCHES>();

}
