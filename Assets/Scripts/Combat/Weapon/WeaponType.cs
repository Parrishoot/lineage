using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class WeaponType : ScriptableObject
{
    public const int INFINITE_AMMO = -1;

    public float cooldown = .5f;
    
    public Sprite sprite;

    public int totalAmmo = INFINITE_AMMO;

    public int ammoPerClip = INFINITE_AMMO;
}
