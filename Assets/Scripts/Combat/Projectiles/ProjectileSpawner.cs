using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : Spawner
{
    public Projectile.PROJECTILE_TARGET_TYPE projectileTargetType;

    public GameObject SpawnProjectile(Vector2 direction, HashSet<ProjectileAttribute.ATTRIBUTE> additionalAttributes = null)
    {
        GameObject obj = SpawnObject();

        obj.GetComponent<Projectile>().Init(direction, projectileTargetType, additionalAttributes);

        return obj;
    }
}
