using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IHitBoxParent
{
    public ProjectileType projectileType;

    private enum PROJECTILE_STATE
    {
        PENDING,
        INITIALIZED,
        FIRING,
        HIT
    }

    private PROJECTILE_STATE projectileState = PROJECTILE_STATE.PENDING;

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
        if (projectileState == PROJECTILE_STATE.FIRING)
        {
            UpdateProjectile();
        }
    }

    public void Init(Vector2 moveDirection, HashSet<ProjectileAttribute.ATTRIBUTE> additionalAttributes = null)
    {
        // Initialize the projectile variables
        direction = moveDirection;
        currentSpawnTime = projectileType.timeBeforeDespawn;
        speed = projectileType.speed;

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
        projectileState = PROJECTILE_STATE.FIRING;

    }

    private void manageAttributes(HashSet<ProjectileAttribute.ATTRIBUTE> allAttributes)
    {
        foreach (ProjectileAttribute.ATTRIBUTE attribute in allAttributes)
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

    public virtual void Despawn()
    {
        // TODO: ADD MORE TO THIS
        Destroy(gameObject);
    }

    public float OnDamageGiven(HurtBox hurtBox)
    {
        projectileState = PROJECTILE_STATE.HIT;

        gameObject.transform.SetParent(hurtBox.gameObject.transform);
        StartCoroutine(BeginDespawn());

        return projectileType.damage;
    }

    IEnumerator BeginDespawn()
    {
        yield return new WaitForSeconds(1f);
        Despawn();
    }
}
