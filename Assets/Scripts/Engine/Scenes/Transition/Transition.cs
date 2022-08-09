using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public string sceneName;
    public int spawnIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            TransitionManager.GetInstance().BeginTransition(sceneName, spawnIndex);
        }
    }
}
