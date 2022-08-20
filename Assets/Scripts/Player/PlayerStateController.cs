using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : StateMachine<PlayerBaseState, PlayerStateController>
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

    public PlayerIdleState playerIdleState;
    public PlayerRunState playerRunState;
    public PlayerDashState playerDashState;
    public PlayerInteractState playerInteractState;

    // Set the animator
    public Animator animator;

    // Sprint particles
    public ParticleSystem sprintParticleSystem;

    private Interactor interactor;
    private Mover mover;

    public override void Start()
    {
        AttachCamera();

        interactor = GetComponent<Interactor>();
        mover = GetComponent<Mover>();

        playerIdleState = new PlayerIdleState();
        playerRunState = new PlayerRunState();
        playerDashState = new PlayerDashState(dashConfig);
        playerInteractState = new PlayerInteractState();

        currentState = playerIdleState;

        tag = PLAYER_TAG;

        base.Start();
    }

    public void SetDash(Vector2 movementVector)
    {
        playerDashState.direction = movementVector;
        SwitchState(playerDashState);
    }

    public Interactor GetInteractor()
    {
        return interactor;
    }

    public Mover GetMover()
    {
        return mover;
    }

    public void AttachCamera()
    {
        CameraController.GetInstance().AttachCameraAnchor(gameObject);
    }
}
