using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public abstract class WeaponController<TState, TController> : StateMachine<TState, TController>
    where TState: WeaponBaseState<TState, TController>
    where TController: WeaponController<TState, TController>
{
    public float cooldown = 1f;
    
    protected SpriteRenderer spriteRenderer;

    public abstract bool IsAiming();

    public virtual float GetAimingAngle()
    {
        return gameObject.transform.eulerAngles.y;
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
        spriteRenderer = GetComponent<SpriteRenderer>();

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
            gameObject.transform.rotation = Quaternion.AngleAxis(-CameraController.getAngleToMouse(gameObject.transform.position) - 270f, Vector3.forward);
        }
    }
}
