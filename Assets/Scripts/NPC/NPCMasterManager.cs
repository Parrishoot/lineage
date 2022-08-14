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
        JOHN
    }

    public Dictionary<NPCType, NPCController> npcControllers = new Dictionary<NPCType, NPCController>();

    public NPCController GetNPCController(NPCType npcName)
    {
        return npcControllers[npcName];
    }

    public void AddNPCController(NPCType npcName, NPCController npcController)
    {
        npcControllers.Add(npcName, npcController);
    }
}
