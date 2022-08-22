using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController<TState, TController> : StateMachine<TState, TController>
    where TState: WeaponBaseState<TState, TController>
    where TController: WeaponController<TState, TController>
{
    public float cooldown = 1f;
    
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public abstract bool IsAiming();

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
        base.Start();
    }

    public override void Update()
    {
        SetRotation();

        base.Update();
    }

    public virtual void SetRotation()
    {
        if (!PauseMenuManager.GetInstance().IsPaused())
        {
            gameObject.transform.rotation  = Quaternion.Euler(0,
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
