using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : Mover
{

    public enum PLAYER_MOVEMENT_STATES
    {
        IDLE,
        RUNNING,
        DASHING,
        INTERACTING
    }

    // DASH variables
    public float dashRampUpPercent = .1f;
    public float dashRampDownPercent = .1f;
    public float dashTotalTime = .1f;
    public float dashCooldown = .5f;
    public float dashSpeedMultiplier = 10f;

    private float currentDashTime = 0f;
    private Vector2 dashDirection = Vector2.zero;
    private float currentDashSpeed;

    // Set the animator
    public Animator animator;
    private const string ANIMATOR_MOVEMENT_PARAMETER_NAME = "playerMovementState";

    // Input Manager
    private InputManager inputManager;

    // Sprint particles
    public ParticleSystem sprintParticleSystem;

    private Interactable interactable = null;

    // Current Movement State
    PLAYER_MOVEMENT_STATES playerMovementState = PLAYER_MOVEMENT_STATES.IDLE;

    private void Start()
    {
        inputManager = InputManager.GetInstance();
        CameraController.GetInstance().AttachCameraAnchor(gameObject);
    }

    private float lastDirection = 0;
    private bool dash = false;

    public void Update()
    {
        if(inputManager.GetKeyDownWithCooldown(InputManager.ACTION.DASH))
        {
            dash = true;
        }
        
        if(inputManager.GetKeyDown(InputManager.ACTION.INTERACT) && 
           interactable != null &&
           playerMovementState != PLAYER_MOVEMENT_STATES.INTERACTING) {

                playerMovementState = PLAYER_MOVEMENT_STATES.INTERACTING;
                rigidbody.velocity = Vector2.zero;
                CameraController.GetInstance().SetConversation(gameObject.gameObject, interactable.gameObject);
                interactable.Interact();

        }
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        // Get the input
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Set movement vector
        Vector2 movementVector = new Vector2(x, y);

        switch (playerMovementState) {

            case PLAYER_MOVEMENT_STATES.IDLE:

                // Dash to the right if IDLE
                if(dash)
                {
                    SetDash(Vector2.right);
                }
                
                // If we are moving, set the state to moving
                else if(!movementVector.Equals(Vector2.zero))
                {
                    playerMovementState = PLAYER_MOVEMENT_STATES.RUNNING;
                    sprintParticleSystem.Play();
                }

                break;

            case PLAYER_MOVEMENT_STATES.RUNNING:

                // Dash in the current direction if Moving
                if (dash)
                {
                    SetDash(Vector2.ClampMagnitude(movementVector, 1));
                }
                
                // If we are no longer moving, set us back to IDLE
                else if (movementVector.Equals(Vector2.zero))
                {
                    playerMovementState = PLAYER_MOVEMENT_STATES.IDLE;
                }

                move(movementVector);

                // Spawn particles if we're changing directions
                if ((x.Equals(1f) && lastDirection.Equals(-1f)) ||
                    (x.Equals(-1f) && lastDirection.Equals(1f)))
                {
                    sprintParticleSystem.Play();
                }

                break;

            case PLAYER_MOVEMENT_STATES.DASHING:

                dash = false;
                currentDashTime -= Time.fixedDeltaTime;

                // TODO: FIX THIS LOGIC
                // Find the ramp and acceleration for the current dash
                float rampUpThreshold = dashTotalTime * dashRampUpPercent;
                float rampDownThreshold = dashTotalTime * dashRampDownPercent;

                float rampAcceleration = ((baseMovementSpeed * dashSpeedMultiplier) - baseMovementSpeed) / rampUpThreshold;
                float rampDeceleration = ((baseMovementSpeed * dashSpeedMultiplier) - baseMovementSpeed) / rampDownThreshold;

                // If we're ramping up, increase the speed
                if (currentDashTime >= (dashTotalTime - rampUpThreshold))
                {
                    currentDashSpeed += (rampAcceleration) * Time.fixedDeltaTime;
                    move(dashDirection, currentDashSpeed);
                }

                // If we're doing our normal dash, maintain the speed but still move
                else if (currentDashTime >= rampDownThreshold)
                {
                    move(dashDirection, currentDashSpeed);
                }

                // If we're ramping down, slow down
                else if (currentDashTime >= 0)
                {
                    currentDashSpeed -= (rampDeceleration) * Time.fixedDeltaTime;
                    move(dashDirection, currentDashSpeed);
                }

                // Otherwise, set us back to our new state
                else
                {
                    if (!movementVector.Equals(Vector2.zero))
                    {
                        playerMovementState = PLAYER_MOVEMENT_STATES.RUNNING;
                    }
                    else
                    {
                        playerMovementState = PLAYER_MOVEMENT_STATES.IDLE;
                        move(movementVector, 0f);
                        sprintParticleSystem.Play();
                    }
                }

                break;

            // Case for when the player begins interaction with an interactable
            case PLAYER_MOVEMENT_STATES.INTERACTING:
                
                if(interactable.InteractionFinished())
                {
                    playerMovementState = PLAYER_MOVEMENT_STATES.IDLE;
                    CameraController.GetInstance().AttachCameraAnchor(gameObject);
                }

                break;

        }

        // Update the animator
        // TODO: Update this to take in a separate player animation enum map value
        Debug.Log((int)playerMovementState);
        animator.SetInteger(ANIMATOR_MOVEMENT_PARAMETER_NAME, (int) playerMovementState);
        lastDirection = x;

        transform.localScale = Mathf.Sign(CameraController.GetVectorToMouse(transform.position).x) >= 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }

    private void SetDash(Vector2 newDashDirection)
    {
        // Set the dash vars
        playerMovementState = PLAYER_MOVEMENT_STATES.DASHING;
        currentDashTime = dashTotalTime;
        dashDirection = newDashDirection;
        currentDashSpeed = baseMovementSpeed;

        // Play the sprint particles
        sprintParticleSystem.Play();

        // Set the cooldown for the dash action
        inputManager.SetKeyCooldown(InputManager.ACTION.DASH, dashCooldown);
    }

    public void AddInteractable(Interactable interactable)
    {
        this.interactable = interactable;
    }

    public void RemoveInteractable(Interactable interactable)
    {
        if(this.interactable.Equals(interactable))
        {
            this.interactable = null;
        }
    }
}
