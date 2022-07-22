using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    public float moveTime;

    private bool move = false;
    private Vector3 targetLocation;
    private Vector3 startLocation;

    private float currentLerpTime = 0f;


    // Update is called once per frame
    void Update()
    {
        // If we're moving
        if(move)
        {
            currentLerpTime += Time.deltaTime / moveTime;
            transform.position = Vector3.Lerp(startLocation, targetLocation, Mathf.SmoothStep(0f, 1f, currentLerpTime));

            if(currentLerpTime >= 1)
            {
                move = false;
            }
        }
    }

    // Sets the target and time to move to the target location
    public void MoveToTarget(Vector3 target, float timeToTarget)
    {
        startLocation = transform.position;
        currentLerpTime = 0f;
        targetLocation = target;
        moveTime = timeToTarget;

        move = true;
    }
}
