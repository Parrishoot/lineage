using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : Mover
{

    public enum PLAYER_STATE
    {
        IDLE,
        RUNNING,
        DASHING,
        INTERACTING
    }

    public const string PLAYER_TAG = "Player";
    public const string ANIMATOR_MOVEMENT_PARAMETER_NAME = "playerMovementState";

    // DASH variables
    public DashConfig dashConfig;

    private PlayerBaseState currentState;

    public PlayerIdleState playerIdleState;
    public PlayerRunState playerRunState;
    public PlayerDashState playerDashState;
    public PlayerInteractState playerInteractState;

    // Set the animator
    public Animator animator;

    // Sprint particles
    public ParticleSystem sprintParticleSystem;

    private Interactable interactable = null;

    private void Start()
    {
        AttachCamera();

        playerIdleState = new PlayerIdleState();
        playerRunState = new PlayerRunState();
        playerDashState = new PlayerDashState(dashConfig);
        playerInteractState = new PlayerInteractState();

        currentState = playerIdleState;
        currentState.EnterState(this);

        tag = PLAYER_TAG;
    }

    public void Update()
    {
        currentState.UpdateState(this);

        float target = 180 - (Mathf.Sign(CameraController.GetVectorToMouse(transform.position).x) + 1) * 90;
        SetFlip(target, Time.fixedDeltaTime);
    }

    // Update is called once per frame
    public void FixedUpdate()
    {

        currentState.FixedUpdateState(this);
        
    }

    public void AddInteractable(Interactable interactable)
    // TODO: Change this to be a trigger on the player instead of the 
    // objects such that we can queue up interactable objects and set an
    // active one rather than resetting this potentially in the middle 
    // of an interaction
    {
        this.interactable = interactable;
    }

    public void SetDash(Vector2 movementVector)
    {
        playerDashState.direction = movementVector;
        SwitchState(playerDashState);
    }

    public Interactable GetCurrentInteractable()
    {
        return interactable;
    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void RemoveInteractable(Interactable interactable)
    {
        if(this.interactable.Equals(interactable))
        {
            this.interactable = null;
        }
    }

    public void AttachCamera()
    {
        CameraController.GetInstance().AttachCameraAnchor(gameObject);
    }
}
