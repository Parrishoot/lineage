using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requires a hitbox collider
[RequireComponent(typeof(Collider2D))]
public class HurtBox : MonoBehaviour
{
    public IHurtBoxParent hurtBoxParent;
    public Shaker shaker;
    public SpriteRenderer spriteRenderer;
    
    public virtual void Start()
    {
        if(hurtBoxParent == null)
        {
            // TODO: Clean this up (but this should be fine as all state controllers should be in parents)
            hurtBoxParent = GetComponentInParent<IHurtBoxParent>();
        }
    }

    public void TakeDamage(float damage)
    {

        hurtBoxParent.OnDamageTaken(damage);
    }

}
