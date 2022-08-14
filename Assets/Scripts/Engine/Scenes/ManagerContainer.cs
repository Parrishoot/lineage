using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerContainer : MonoBehaviour
{
    // Runs before a scene gets loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadMain()
    {
        if (GameObject.Find("ManagerContainer") == null)
        {
            GameObject main = GameObject.Instantiate(Resources.Load("Prefabs/Managers/ManagerContainer")) as GameObject;
            main.name = "ManagerContainer";
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
