using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{

    public Vector2 direction;

    private DashConfig dashConfig;

    private float time = 0f;
    private float speed = 0f;

    public PlayerDashState(DashConfig dashConfig)
    {
        this.dashConfig = dashConfig;
    }

    public override void EnterState(PlayerStateController controller)
    {
        time = dashConfig.totalTime;
        speed = controller.baseMovementSpeed;

        InputManager.GetInstance().SetKeyCooldown(InputManager.ACTION.DASH, dashConfig.cooldown);

        controller.animator.SetInteger(PlayerStateController.ANIMATOR_MOVEMENT_PARAMETER_NAME, (int) PlayerStateController.PLAYER_STATE.DASHING);
    }

    public override void ExitState(PlayerStateController controller)
    {
        
    }

    public override void FixedUpdateState(PlayerStateController controller)
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
            controller.Move(direction, speed);
        }

        // If we're doing our normal dash, maintain the speed but still move
        else if (time >= rampDownThreshold)
        {
            controller.Move(direction, speed);
        }

        // If we're ramping down, slow down
        else if (time >= 0)
        {
            speed -= (rampDeceleration) * Time.fixedDeltaTime;
            controller.Move(direction, speed);
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
                controller.Move(movementVector, 0f);
                controller.sprintParticleSystem.Play();
            }
        }

    }

    public override void UpdateState(PlayerStateController controller)
    {
        
    }
}
