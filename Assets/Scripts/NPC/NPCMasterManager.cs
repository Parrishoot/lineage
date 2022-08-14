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

    public void OnEnable()
    {
        ResetControllers();
    }

    public NPCController GetNPCController(NPCType npcName)
    {
        return npcControllers.ContainsKey(npcName) ? npcControllers[npcName] : null;
    }

    public void ResetControllers()
    {
        npcControllers = new Dictionary<NPCType, NPCController>();

        foreach(NPCController controller in FindObjectsOfType<NPCController>())
        {
            if(controller.GetNPCType() != NPCType.NOBODY)
            {
                AddNPCController(controller.GetNPCType(),
                 controller);
            }
        }
    }

    public void AddNPCController(NPCType npcName, NPCController npcController)
    {
        npcControllers.Add(npcName, npcController);
    }
}
