using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipper : Flipper
{

    // Update is called once per frame
    void FixedUpdate()
    {
        float target = 180 - (Mathf.Sign(CameraController.GetVectorToMouse(transform.position).x) + 1) * 90;
        SetFlip(target, Time.fixedDeltaTime);
    }
}
