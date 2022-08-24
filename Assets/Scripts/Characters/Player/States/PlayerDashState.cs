using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState<TStateMachine> : PlayerBaseState<TStateMachine>
    where TStateMachine : PlayerStateController<TStateMachine>
{

    public Vector2 direction;

    private DashConfig dashConfig;

    private float time = 0f;
    private float speed = 0f;

    public PlayerDashState(DashConfig dashConfig)
    {
        this.dashConfig = dashConfig;
    }

    public override void EnterState(TStateMachine controller)
    {
        time = dashConfig.totalTime;
        speed = controller.GetMover().baseMovementSpeed;

        InputManager.GetInstance().SetKeyCooldown(InputManager.ACTION.DASH, dashConfig.cooldown);

        controller.animator.SetInteger(PlayerMeta.ANIMATOR_MOVEMENT_PARAMETER_NAME, (int) PlayerStateController<TStateMachine>.PLAYER_STATE.DASHING);
    }

    public override void ExitState(TStateMachine controller)
    {
        
    }

    public override void FixedUpdateState(TStateMachine controller)
    {
        Vector2 movementVector = GetMovementVector();

        time -= Time.fixedDeltaTime;


        // Find the ramp and acceleration for the current dash
        float rampUpThreshold = dashConfig.totalTime * dashConfig.rampUpPercent;
        float rampDownThreshold = dashConfig.totalTime * dashConfig.rampDownPercent;

        float rampAcceleration = ((speed * dashConfig.speedMultiplier) - speed) / rampUpThreshold;
        float rampDeceleration = ((speed * dashConfig.speedMultiplier) - speed) / rampDownThreshold;

        // If we're ramping up, increase the speed
        if (time >= (dashConfig.totalTime - rampUpThreshold))
        {
            speed += (rampAcceleration) * Time.fixedDeltaTime;
            controller.GetMover().Move(direction, speed);
        }

        // If we're doing our normal dash, maintain the speed but still move
        else if (time >= rampDownThreshold)
        {
            controller.GetMover().Move(direction, speed);
        }

        // If we're ramping down, slow down
        else if (time >= 0)
        {
            speed -= (rampDeceleration) * Time.fixedDeltaTime;
            controller.GetMover().Move(direction, speed);
        }

        // Otherwise, set us back to our new state
        else
        {
            if (!movementVector.Equals(Vector2.zero))
            {
                controller.SwitchState(controller.playerRunState);
            }
            else
            {
                controller.SwitchState(controller.playerIdleState);
                controller.GetMover().Move(movementVector, 0f);
                controller.sprintParticleSystem.Play();
            }
        }

    }

    public override void UpdateState(TStateMachine controller)
    {
        
    }
}
