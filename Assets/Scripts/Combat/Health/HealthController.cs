using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController: MonoBehaviour
{

    public float health = 100;
    
    public Shaker shaker;
    public SpriteRenderer spriteRenderer;

    private bool isShaking = false;

    public const float FLASH_ITERATION_TIME = .1f;

    public void Start()
    {
        if(shaker == null)
        {
            shaker = GetComponent<Shaker>();
        }

        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void Damage(float damage)
    {
        health -= damage;

        if(!IsDead())
        {
            StartCoroutine(FlashDamageColor());
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public bool DamageIndicatorActive()
    {
        return isShaking;
    }

    IEnumerator FlashDamageColor()
    {
        isShaking = true;

        shaker.SetShake();

        while (shaker.IsShaking())
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(FLASH_ITERATION_TIME);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(FLASH_ITERATION_TIME);
        }
        spriteRenderer.color = Color.white;

        isShaking = false;
    }

}
