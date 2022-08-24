using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class WeaponType : ScriptableObject
{

    public const int INFINITE_AMMO = -1;

    public float cooldown = .5f;
    
    public Sprite sprite;

    public float totalAmmo = INFINITE_AMMO;

    public float ammoPerClip = INFINITE_AMMO;

    public ProjectileType projectileType;

    public GameObject firingMechanismPrefab;
}
