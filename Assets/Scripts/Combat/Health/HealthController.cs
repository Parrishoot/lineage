using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController: MonoBehaviour
{

    public float health = 100;

    public void Damage(float damage)
    {
        health -= damage;
    }

    public bool IsDead()
    {
        return health <= 0;
    }

}
