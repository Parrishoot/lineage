using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Projectile")]
public class ProjectileType : ScriptableObject
{
    public Sprite sprite;

    public float damage;

    public float speed;

    public float timeBeforeDespawn = 3f;

    // TODO: See if I can convert this to a Set somehow and maintain the ability to make these in the editor
    public List<ProjectileAttribute.ATTRIBUTE> projectileAttributes = new List<ProjectileAttribute.ATTRIBUTE>();

}

