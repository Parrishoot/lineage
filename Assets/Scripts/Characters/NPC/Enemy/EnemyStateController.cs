using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class EnemyStateController<T> : NPCStateController<T>, IHurtBoxParent
    where T : EnemyStateController<T>
{
    public HealthController healthController;

    public enum ENEMY_MOVEMENT_TYPE
    {
        RANDOM,
        AGGRESSIVE,
        SCAREDY_CAT
    }

    public ENEMY_MOVEMENT_TYPE enemyMovementType;

    public EnemyDamageState<T> enemyDamageState = new EnemyDamageState<T>();

    public override void Start()
    {
        healthController = GetComponent<HealthController>();

        base.Start();
    }

    public Vector2 GetDirectonToPlayer()
    {
        return (GameObject.FindGameObjectWithTag(PlayerMeta.PLAYER_TAG).transform.position - gameObject.transform.position).normalized;
    }

    public override Vector3 FindNewDestination(bool beginMove=false)
    {
        switch(enemyMovementType)
        {
            case ENEMY_MOVEMENT_TYPE.RANDOM:
                return SetRandomWander(beginMove);

            // TODO: UPDATE THESE BEHAVIORS
            case ENEMY_MOVEMENT_TYPE.AGGRESSIVE:
                return SetRandomWander(beginMove);

            case ENEMY_MOVEMENT_TYPE.SCAREDY_CAT:
                return SetRandomWander(beginMove);

            default:
                return SetRandomWander(beginMove);
        }
    }

    public void OnDamageTaken(float damage)
    {
        healthController.Damage(damage);

        // TODO: ADD A DEATH STATE/CASE HERE
        if(healthController.IsDead())
        {
            SwitchState(enemyDamageState);
        }
        else
        {
            SwitchState(enemyDamageState);
        }

    }
}


