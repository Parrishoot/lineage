using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputManager : Singleton<InputManager>
{
    public List<InputAction> inputs;

    public Dictionary<ACTION, InputAction> inputKeys = new Dictionary<ACTION, InputAction>();

    private Dictionary<ACTION, float> keyCooldowns = new Dictionary<ACTION, float>();

    public enum ACTION
    {
        DASH,
        SHOOT,
        INTERACT,
        DIALOGUE_CONTINUE,
        PAUSE
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
        InputAction inputAction = inputKeys[action];

        if(!PauseMenuManager.GetInstance().IsPaused() || inputAction.allowWhilePaused)
        {
            return Input.GetKeyDown(inputAction.keyCode);
        }
        else
        {
            return false;
        }
        
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
        InputAction inputAction = inputKeys[action];

        if (!PauseMenuManager.GetInstance().IsPaused() || inputAction.allowWhilePaused)
        {
            return Input.GetKeyUp(inputAction.keyCode);
        }
        else
        {
            return false;
        }
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
        InputAction inputAction = inputKeys[action];

        if (!PauseMenuManager.GetInstance().IsPaused() || inputAction.allowWhilePaused)
        {
            return Input.GetKey(inputAction.keyCode);
        }
        else
        {
            return false;
        }
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
        foreach(InputAction action in inputs) {
            SetKey(action.actionType, action);
        }
    }

    public void SetKey(ACTION action, InputAction inputAction)
    {
        if(inputKeys.ContainsKey(action))
        {
            inputKeys[action] = inputAction;
        }
        else
        {
            inputKeys.Add(action, inputAction);
        }
    }

    [System.Serializable]
    public class InputAction
    {
        public InputManager.ACTION actionType;
        public KeyCode keyCode;
        public bool allowWhilePaused = false;
    }
}

