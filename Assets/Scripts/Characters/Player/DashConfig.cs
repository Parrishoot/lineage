using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DashConfig
{
    public float rampUpPercent = .5f;
    public float rampDownPercent = .5f;
    public float totalTime = .15f;
    public float cooldown = .7f;
    public float speedMultiplier = 3f;
}
