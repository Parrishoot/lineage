using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFiringMechanism : FiringMechanismController
{

    public float powerShotThresholdTime = 1f;
    private float currentShotHeldTime;

    public Transform pathStart;
    public Transform pathEnd;

    public GameObject superShotZone;
    public GameObject currentShotIndicator;

    public GameObject progressBarParent;

    public float indicatorMovementSpeed;
    public float superShotZoneBorderThreshold = .2f;

    private SuperShotZoneController superShotZoneController;

    private InputManager inputManager;

    public override void Start()
    {

        base.Start();

        currentShotHeldTime = powerShotThresholdTime;

        superShotZoneController = superShotZone.GetComponent<SuperShotZoneController>();

        transform.parent = null;

        inputManager = InputManager.GetInstance();
    }

    public override void Update()
    {

        base.Update();

        if (isIdle()) { 

            // TODO: Change this to use the InputManager cooldown system
            // Instead of splitting it between WeaponController and firing mechanism
            if(inputManager.GetKeyDown(InputManager.ACTION.SHOOT))
            {
                firingMechanismState = FIRING_MECHANISM_STATES.PENDING;
                currentShotHeldTime = powerShotThresholdTime;
                ResetPositions();
                progressBarParent.SetActive(true);
            }

        }
        else if (isPending())
        {
            if (inputManager.GetKey(InputManager.ACTION.SHOOT))
            {

                currentShotIndicator.transform.position += Vector3.right * indicatorMovementSpeed * Time.deltaTime;

            }
            else if (inputManager.GetKeyUp(InputManager.ACTION.SHOOT))
            {
                firingMechanismState = superShotZoneController.isSuccessfulCollision() ? FIRING_MECHANISM_STATES.POWER_SHOT : FIRING_MECHANISM_STATES.REGULAR_SHOT;
                progressBarParent.SetActive(false);
            }
        }

        if(currentShotIndicator.transform.position.x > pathEnd.position.x)
        {
            firingMechanismState = FIRING_MECHANISM_STATES.FAILURE;
            progressBarParent.SetActive(false);
        }
    }

    private void ResetPositions()
    {
        currentShotIndicator.transform.position = pathStart.position;

        // Choose a new position for the super shot indicator
        float pathLength = pathEnd.position.x - pathStart.position.x;
        superShotZone.transform.position = new Vector3(Random.Range(pathStart.position.x + (superShotZoneBorderThreshold * pathLength), pathEnd.position.x), superShotZone.transform.position.y, 0);

    }

}
