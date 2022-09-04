using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject spawnPrefab;

    public GameObject SpawnObject()
    {
        return SpawnObject(transform.position);
    }

    public virtual GameObject SpawnObject(Vector3 position)
    {
        return Instantiate(spawnPrefab, position, Quaternion.identity);
    }
}
