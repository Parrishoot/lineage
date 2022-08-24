using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requires a hitbox collider
[RequireComponent(typeof(Collider2D))]
public class HitBox : Shakeable
{

    public HealthController healthController;

    public SpriteRenderer spriteRenderer;

    private const float FLASH_ITERATION_TIME = .1f;
    
    public virtual void Start()
    {
        if(healthController == null)
        {
            healthController = gameObject.GetComponent<HealthController>();
        }
    }

    public void TakeDamage(float damage)
    {

        healthController.Damage(damage);

        if(!healthController.IsDead())
        {
            StartCoroutine(FlashDamageColor());
        }
    }
   
    IEnumerator FlashDamageColor()
    {
        SetShake();

        while(IsShaking())
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(FLASH_ITERATION_TIME);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(FLASH_ITERATION_TIME);
        }
        spriteRenderer.color = Color.white;
    }
}
