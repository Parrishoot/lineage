using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requires a hitbox collider
[RequireComponent(typeof(Collider2D))]


public class Damageable : Shakeable
{

    public float maxHealth;
    public SpriteRenderer spriteRenderer;

    private const float FLASH_ITERATION_TIME = .1f;

    private float currentHealth;
    
    public virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        print("DAMAGE");
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Death();
        }
        else
        {
            StartCoroutine(FlashDamageColor());
        }
    }

    public override void Update()
    {
        base.Update();
    }

    public void Death()
    {
        // TODO: CHANGE THIS TO BE MORE THAN JUST DISAPPEARING
        Destroy(gameObject);
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
