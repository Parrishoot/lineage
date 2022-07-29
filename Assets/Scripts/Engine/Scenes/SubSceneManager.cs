using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Scenes;

public class SubSceneManager : Singleton<SubSceneManager>
{
    public float loadRadius = 10f;

    public SubScene[] subSceneReferences;

}
