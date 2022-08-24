using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum PROJECTILE_TARGET_TYPE
    {
        ENEMY,
        PLAYER
    }

    private PROJECTILE_TARGET_TYPE projectileTargetType;

    public ProjectileType projectileType;

    private HashSet<ProjectileAttribute.ATTRIBUTE> appliedAttributes = new HashSet<ProjectileAttribute.ATTRIBUTE>();

    private Vector2 direction;
    private bool initialized = false;
    private float currentSpawnTime;
    private float speed;

    public virtual void UpdateProjectile()
    {
        currentSpawnTime -= Time.deltaTime;

        if (currentSpawnTime <= 0)
        {
            Despawn();
        }

        Vector3 movePosition = direction.normalized * Time.deltaTime * speed;
        gameObject.transform.position += movePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            UpdateProjectile();
        }
    }

    public void Init(Vector2 moveDirection, PROJECTILE_TARGET_TYPE newProjectileTargetType, HashSet<ProjectileAttribute.ATTRIBUTE> additionalAttributes = null)
    {
        // Initialize the projectile variables
        direction = moveDirection;
        currentSpawnTime = projectileType.timeBeforeDespawn;
        speed = projectileType.speed;
        projectileTargetType = newProjectileTargetType;

        // Rotate towards aiming position
        transform.rotation = Quaternion.AngleAxis(-CameraController.getAngleToMouse(gameObject.transform.position), Vector3.forward);

        // Sets the projectile attributes to add
        HashSet<ProjectileAttribute.ATTRIBUTE> allAttributes = new HashSet<ProjectileAttribute.ATTRIBUTE>(projectileType.projectileAttributes);
        if (additionalAttributes != null)
        {
            allAttributes.UnionWith(additionalAttributes);
        }
        manageAttributes(allAttributes);

        // Set the projectile initialized
        initialized = true;

    }

    private void manageAttributes(HashSet<ProjectileAttribute.ATTRIBUTE> allAttributes)
    {
        foreach(ProjectileAttribute.ATTRIBUTE attribute in allAttributes)
        {
            switch (attribute)
            {
                case ProjectileAttribute.ATTRIBUTE.SPEED_SHOT:
                    speed *= 4;
                    break;

                // TODO: ADD MORE CASES FOR NEW ATTRIBUTES
            }

            appliedAttributes.Add(attribute);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (projectileTargetType)
        {
            case PROJECTILE_TARGET_TYPE.PLAYER:
                
                PlayerHitBox playerHitBox = collision.gameObject.GetComponent<PlayerHitBox>();

                if(playerHitBox != null)
                {
                    playerHitBox.TakeDamage(projectileType.damage);
                }

                break;

            case PROJECTILE_TARGET_TYPE.ENEMY:

                EnemyHitBox enemyHitBox = collision.gameObject.GetComponent<EnemyHitBox>();

                if (enemyHitBox != null)
                {
                    enemyHitBox.TakeDamage(projectileType.damage);
                }

                break;
        }
    }
    public virtual void Despawn()
    {
        // TODO: ADD MORE TO THIS
        Destroy(gameObject);
    }
}
