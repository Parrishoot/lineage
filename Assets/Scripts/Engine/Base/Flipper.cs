using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{

    public float flipSpeed = 1000f;

    public void SetFlip(float target, float deltaTime)
    {

        float rotationDifference = target - transform.eulerAngles.y;

        if (!rotationDifference.Equals(0))
        {
            float rotationMultiplier = Mathf.Sign(rotationDifference);

            float yRotator = rotationMultiplier * flipSpeed * deltaTime;

            yRotator = Mathf.Abs(rotationDifference) < Mathf.Abs(yRotator) ? rotationDifference : yRotator;

            transform.Rotate(0, yRotator, 0);


            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                Mathf.Clamp(transform.eulerAngles.y, 0, 180),
                                                transform.eulerAngles.z);
        }
    }
}
