using System.Collections;
using Unity.Entities;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Scenes;

public class SubSceneLoader : ComponentSystem
{

    public float loadRadius = 10f;


    private SceneSystem sceneSystem;

    // Start is called before the first frame update
    protected override void OnCreate()
    {
        sceneSystem = World.GetOrCreateSystem<SceneSystem>();
    }

    private void LoadSubScene(SubScene subScene)
    {
        sceneSystem.LoadSceneAsync(subScene.SceneGUID);
    }

    private void UnloadSubScene(SubScene subScene)
    {
        Debug.Log(subScene.SceneName);
        sceneSystem.UnloadScene(subScene.SceneGUID);
    }

    protected override void OnUpdate()
    {
        foreach (GameObject playerObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            foreach(SubScene subScene in SubSceneManager.GetInstance().subSceneReferences)
            {
                if (math.distance(playerObject.transform.position, subScene.transform.position) <= SubSceneManager.GetInstance().loadRadius)
                {
                    LoadSubScene(subScene);
                }
                else
                {
                    UnloadSubScene(subScene);
                }
            }
        }
    }
}
