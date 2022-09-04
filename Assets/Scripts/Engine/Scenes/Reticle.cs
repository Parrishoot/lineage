using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    // Runs before a scene gets loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadMain()
    {
        if (GameObject.Find("Reticle") == null)
        {
            GameObject main = GameObject.Instantiate(Resources.Load("Prefabs/Reticle")) as GameObject;
            main.name = "Reticle";
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        // Follows the player's mouse
        transform.position = CameraController.GetMousePosition();
        Cursor.visible = false;
    }
}
