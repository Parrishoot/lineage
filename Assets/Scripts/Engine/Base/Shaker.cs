using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public const float DEFAULT_SHAKE_AMOUNT = .05f;
    public const float DEFAULT_SHAKE_TIME = .15f;
    public const float DEFAULT_SHAKE_SPEED = 100f;

    private float currentShakeAmount = DEFAULT_SHAKE_AMOUNT;
    private float currentShakeSpeed = DEFAULT_SHAKE_AMOUNT;

    private float currentShakeTime = -1f;
    private bool shaking = false;

    private Vector3 startingPosition;

    public virtual void Update()
    {
        if (shaking)
        {
            currentShakeTime -= Time.deltaTime;
            if (currentShakeTime <= 0)
            {
                shaking = false;
                transform.position = startingPosition;
            }
            else
            {
                gameObject.transform.position = new Vector3(startingPosition.x + Mathf.Sin(Time.time * currentShakeSpeed) * currentShakeAmount,
                                                            startingPosition.y + Mathf.Sin(Time.time * currentShakeSpeed) * currentShakeAmount,
                                                            0);
            }
        }
    }

    public void SetShake(float shakeAmount = DEFAULT_SHAKE_AMOUNT, float shakeTime = DEFAULT_SHAKE_TIME, float shakeSpeed = DEFAULT_SHAKE_SPEED)
    {
        currentShakeTime = shakeTime;
        currentShakeAmount = shakeAmount;
        currentShakeSpeed = shakeSpeed;

        startingPosition = gameObject.transform.position;

        shaking = true;
    }

    public bool IsShaking()
    {
        return shaking;
    }
}
