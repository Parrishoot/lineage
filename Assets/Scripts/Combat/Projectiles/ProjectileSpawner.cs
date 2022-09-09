using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : Spawner
{

    public GameObject SpawnProjectile(Vector2 direction, HashSet<ProjectileAttribute.ATTRIBUTE> additionalAttributes = null)
    {
        GameObject obj = SpawnObject();

        obj.GetComponent<Projectile>().Init(direction, additionalAttributes);

        return obj;
    }
}
