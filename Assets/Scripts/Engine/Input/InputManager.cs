using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputManager : Singleton<InputManager>
{

    public Dictionary<ACTION, KeyCode> inputKeys = new Dictionary<ACTION, KeyCode>();

    private Dictionary<ACTION, float> keyCooldowns = new Dictionary<ACTION, float>();

    public enum ACTION
    {
        DASH,
        SHOOT,
        INTERACT,
        DIALOGUE_CONTINUE
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeKeys();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: This is a hack for now I hate having to loop
        // Through these keys twice it feels so dirty
        List<ACTION> keysToRemove = new List<ACTION>();
        foreach(ACTION action in keyCooldowns.Keys.ToList())
        {
            keyCooldowns[action] -= Time.deltaTime;
            if(keyCooldowns[action] <= 0)
            {
                keyCooldowns.Remove(action);
            }
        }
    }

    public bool GetKeyDown(ACTION action)
    {
        return Input.GetKeyDown(inputKeys[action]);
    }

    public bool GetKeyDownWithCooldown(ACTION action)
    {
        if(keyCooldowns.ContainsKey(action))
        {
            return false;
        }
        else
        {
            return GetKeyDown(action);
        }
    }

    public bool GetKeyUp(ACTION action)
    {
        return Input.GetKeyUp(inputKeys[action]);
    }

    public bool GetKeyUpWithCooldown(ACTION action)
    {
        if (keyCooldowns.ContainsKey(action))
        {
            return false;
        }
        else
        {
            return GetKeyUp(action);
        }
    }

    public bool GetKey(ACTION action)
    {
        return Input.GetKey(inputKeys[action]);
    }

    public bool GetKeyWithCooldown(ACTION action)
    {
        if (keyCooldowns.ContainsKey(action))
        {
            return false;
        }
        else
        {
            return GetKey(action);
        }
    }

    public void SetKeyCooldown(ACTION action, float cooldown)
    {
        keyCooldowns[action] = cooldown;
    }

    public void InitializeKeys()
    {
        /*
         * 
         * THIS IS WHERE TO ADD NEW INPUT KEYS 
         * 
         */
        SetKey(ACTION.DASH, KeyCode.LeftShift);
        SetKey(ACTION.SHOOT, KeyCode.Mouse0);
        SetKey(ACTION.INTERACT, KeyCode.E);
        SetKey(ACTION.DIALOGUE_CONTINUE, KeyCode.Space);
    }

    public void SetKey(ACTION action, KeyCode keyCode)
    {
        if(inputKeys.ContainsKey(action))
        {
            inputKeys[action] = keyCode;
        }
        else
        {
            inputKeys.Add(action, keyCode);
        }
    }
}
