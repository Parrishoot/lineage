using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeaponStateController<TStateMachine>: WeaponStateController<TStateMachine>, IWeapon
    where TStateMachine: PlayerWeaponStateController<TStateMachine>
{
    public abstract bool IsAiming();

    public abstract void Reset();

    public override void Update()
    {
        SetRotation();

        base.Update();
    }

    public override void Shoot(HashSet<ProjectileAttribute.ATTRIBUTE> attributes = null)
    {

        Vector2 direction = CameraController.GetVectorToMouse(gameObject.transform.position).normalized;

        projectileSpawner.SpawnProjectile(direction, attributes);
    }

    public virtual void SetRotation()
    {
        if (!PauseMenuManager.GetInstance().IsPaused())
        {
            gameObject.transform.rotation = Quaternion.Euler(0,
                                                              0,
                                                              -CameraController.getAngleToMouse(gameObject.transform.position) - 270f);

            if (GetAimingAngle() <= 270 && GetAimingAngle() >= 90)
            {
                transform.localScale = new Vector3(transform.localScale.x,
                                                   -1,
                                                   transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x,
                                                   1,
                                                   transform.localScale.z);
            }
        }
    }
}
