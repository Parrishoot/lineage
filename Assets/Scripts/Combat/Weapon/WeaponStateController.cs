using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponStateController<TStateMachine> : StateMachine<TStateMachine>
    where TStateMachine: WeaponStateController<TStateMachine>
{
    public WeaponType weaponType;

    
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public ProjectileSpawner projectileSpawner;

    [HideInInspector]
    public float cooldown;

    [HideInInspector]
    public int ammoPerClip;

    [HideInInspector]
    public int totalAmmo;

    [HideInInspector]
    public ProjectileType projectileType;

    public virtual float GetAimingAngle()
    {
        return gameObject.transform.eulerAngles.z;
    }

    public virtual void SetInvisible()
    {
        spriteRenderer.enabled = false;
    }

    public virtual void SetVisible()
    {
        spriteRenderer.enabled = true;
    }

    public override void Start()
    {
        spriteRenderer.sprite = weaponType.sprite;
        cooldown = weaponType.cooldown;
        ammoPerClip = weaponType.ammoPerClip;
        totalAmmo = weaponType.totalAmmo;

        base.Start();
    }

    public abstract void Shoot(HashSet<ProjectileAttribute.ATTRIBUTE> attributes = null);
}
