using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFlipper : Flipper
{
    float flipTarget = 0f;

    // Update is called once per frame
    void Update()
    {
        SetFlip(flipTarget, Time.deltaTime);
    }

    public void SetFlipTarget(float newFlipTarget)
    {
        flipTarget = newFlipTarget;
    }
}
