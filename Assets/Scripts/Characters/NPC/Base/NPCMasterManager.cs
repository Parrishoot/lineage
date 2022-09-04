using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMasterManager : Singleton<NPCMasterManager>
{
    public enum NPCType
    {
        NOBODY,

        GARFIELD,
        ODIE,
        JOHN,

        TREVOR,
        BRAD
    }

    public Dictionary<NPCType, DialogueInteractable> npcControllers = new Dictionary<NPCType, DialogueInteractable>();

    public void OnEnable()
    {
        ResetControllers();
    }

    public DialogueInteractable GetNPCController(NPCType npcName)
    {
        return npcControllers.ContainsKey(npcName) ? npcControllers[npcName] : null;
    }

    public void ResetControllers()
    {
        npcControllers = new Dictionary<NPCType, DialogueInteractable>();

        foreach(FriendPCStateController controller in FindObjectsOfType<FriendPCStateController>())
        {
            if(controller.npcType != NPCType.NOBODY)
            {
                AddNPCController(controller.npcType,
                                 controller.GetDialogueInteractable());
            }
        }
    }

    public void AddNPCController(NPCType npcName, DialogueInteractable npcController)
    {
        npcControllers.Add(npcName, npcController);
    }
}
