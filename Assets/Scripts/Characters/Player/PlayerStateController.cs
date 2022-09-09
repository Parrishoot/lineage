using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController<TStateMachine>: StateMachine<TStateMachine>, IHurtBoxParent
    where TStateMachine: PlayerStateController<TStateMachine>
{
    // Hack to get around the interface issue

    public GameObject weaponObject;

    public IWeapon weapon;

    public enum PLAYER_STATE
    {
        IDLE,
        RUNNING,
        DASHING,
        INTERACTING,
        AIM
    }

    // DASH variables
    public DashConfig dashConfig;

    public PlayerIdleState<TStateMachine> playerIdleState = new PlayerIdleState<TStateMachine>();
    public PlayerRunState<TStateMachine> playerRunState = new PlayerRunState<TStateMachine>();
    public PlayerDashState<TStateMachine> playerDashState;
    public PlayerInteractState<TStateMachine> playerInteractState = new PlayerInteractState<TStateMachine>();

    // Set the animator
    public Animator animator;

    // Health Controller
    public HealthController healthController;

    // Sprint particles
    public ParticleSystem sprintParticleSystem;

    private Interactor interactor;
    private Mover mover;

    public override void Start()
    {
        AttachCamera();

        weapon = weaponObject.GetComponent<IWeapon>();

        interactor = GetComponent<Interactor>();
        mover = GetComponent<Mover>();
        healthController = GetComponent<HealthController>();

        playerDashState = new PlayerDashState<TStateMachine>(dashConfig);

        currentState = playerIdleState;

        tag = PlayerMeta.PLAYER_TAG;

        base.Start();
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

    public void SetDash(Vector2 movementVector)
    {
        playerDashState.direction = movementVector;
        SwitchState(playerDashState);
    }

    public void OnDamageTaken(float damageTaken)
    {
        healthController.Damage(1);

        // TODO: ADD DAMAGE STATE
    }
}
