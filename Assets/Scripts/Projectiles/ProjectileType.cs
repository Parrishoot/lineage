using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Projectile")]
public class ProjectileType : ScriptableObject
{
    public Sprite sprite;

    public float damage;

    public float speed;

    // TODO: See if I can convert this to a Set somehow and maintain the ability to make these in the editor
    public List<ProjectileAttributes.ATTRIBUTES> projectileAttributes = new List<ProjectileAttributes.ATTRIBUTES>();

}

