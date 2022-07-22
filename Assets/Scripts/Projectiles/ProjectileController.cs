using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private const float DEFAULT_TIME_BEFORE_DESPAWN = 3f;

    public ProjectileType projectileType;

    private HashSet<ProjectileAttributes.ATTRIBUTES> appliedAttributes = new HashSet<ProjectileAttributes.ATTRIBUTES>();

    private Vector2 direction;
    private bool initialized = false;
    private float currentSpawnTime;
    private float damage;
    private float speed;
    private string targetTag = "EnemyHitBox";

    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            currentSpawnTime -= Time.deltaTime;

            if (currentSpawnTime <= 0)
            {
                Despawn();
            }

            Vector3 movePosition = direction.normalized * Time.deltaTime * speed;
            gameObject.transform.position += movePosition;
        }
    }

    public void InitializeValues(ProjectileType newProjectileType, Vector2 moveDirection, HashSet<ProjectileAttributes.ATTRIBUTES> additionalAttributes = null, float timeBeforeDespawnOverride = DEFAULT_TIME_BEFORE_DESPAWN, string newTargetTag = "EnemyHitBox")
    {
        // Initialize the projectile variables
        projectileType = newProjectileType;
        direction = moveDirection;
        currentSpawnTime = timeBeforeDespawnOverride;
        speed = newProjectileType.speed;
        damage = newProjectileType.damage;
        targetTag = newTargetTag;

        // Rotate towards aiming position
        transform.rotation = Quaternion.AngleAxis(-CameraController.getAngleToMouse(gameObject.transform.position), Vector3.forward);

        // Sets the projectile attributes to add
        HashSet<ProjectileAttributes.ATTRIBUTES> allAttributes = new HashSet<ProjectileAttributes.ATTRIBUTES>(newProjectileType.projectileAttributes);
        if (additionalAttributes != null)
        {
            allAttributes.UnionWith(additionalAttributes);
        }
        manageAttributes(allAttributes);

        // Set the projectile initialized
        initialized = true;

    }

    private void manageAttributes(HashSet<ProjectileAttributes.ATTRIBUTES> allAttributes)
    {
        foreach(ProjectileAttributes.ATTRIBUTES attribute in allAttributes)
        {
            switch (attribute)
            {
                case ProjectileAttributes.ATTRIBUTES.SPEED_SHOT:
                    speed *= 4;
                    damage *= 2;
                    break;

                // TODO: ADD MORE CASES FOR NEW ATTRIBUTES
            }

            appliedAttributes.Add(attribute);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(targetTag))
        {
            collision.GetComponent<Damageable>().TakeDamage(damage);
            Despawn();
        }
    }

    public virtual void Despawn()
    {
        // TODO: ADD MORE TO THIS
        Destroy(gameObject);
    }
}
