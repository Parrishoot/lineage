using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    public IHitBoxParent hitBoxParent;

    public enum HIT_BOX_TARGET_TYPE
    {
        ENEMY,
        PLAYER
    }

    public HIT_BOX_TARGET_TYPE hitBoxTargetType;

    public void Start()
    {
        if(hitBoxParent == null)
        {
            hitBoxParent = GetComponentInParent<IHitBoxParent>();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (hitBoxTargetType)
        {
            case HIT_BOX_TARGET_TYPE.PLAYER:

                PlayerHurtBox playerHurtBox = collision.gameObject.GetComponent<PlayerHurtBox>();

                if (playerHurtBox != null)
                {
                    playerHurtBox.TakeDamage(hitBoxParent.OnDamageGiven(playerHurtBox));
                }

                break;

            case HIT_BOX_TARGET_TYPE.ENEMY:

                EnemyHurtBox enemyHurtBox = collision.gameObject.GetComponent<EnemyHurtBox>();

                if (enemyHurtBox != null)
                {
                    enemyHurtBox.TakeDamage(hitBoxParent.OnDamageGiven(enemyHurtBox));
                }

                break;
        }
    }
}
