using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWeaponController : MonoBehaviour
{

    public WeaponType weapon;
    public SpriteRenderer spriteRenderer;

    public GameObject projectilePrefab;

    private float currentCooldown;
    private FiringMechanismController firingMechanismController;

    // Start is called before the first frame update
    void Start()
    {
        changeWeaponType(weapon);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCooldown <= 0 && (firingMechanismController.isResetting() || firingMechanismController.isFailure()))
        {
            firingMechanismController.SetIdle();
        }
        
        if(firingMechanismController.isPowerShot() || firingMechanismController.isRegularShot())
        {
            // Check for power shot to add new attributes
            HashSet<ProjectileAttributes.ATTRIBUTES> addedAttributes = new HashSet<ProjectileAttributes.ATTRIBUTES>();
            if(firingMechanismController.isPowerShot())
            {
                addedAttributes.Add(ProjectileAttributes.ATTRIBUTES.SPEED_SHOT);
            }

            // Create the projectile and initialize it
            GameObject projectileObject = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
            ProjectileController projectileController = projectileObject.GetComponent<ProjectileController>();

            projectileController.InitializeValues(weapon.projectileType, CameraController.GetVectorToMouse(gameObject.transform.position), addedAttributes);

            // Reset the cooldown
            currentCooldown = weapon.cooldown;
            firingMechanismController.Reset();
        }
        
        currentCooldown = Mathf.Max(currentCooldown - Time.deltaTime, 0);

        if(!PauseMenuManager.GetInstance().IsPaused())
        {
            gameObject.transform.rotation = Quaternion.AngleAxis(-CameraController.getAngleToMouse(gameObject.transform.position) - 270f, Vector3.forward);
        }
    }

    public void changeWeaponType(WeaponType weaponType)
    {
        spriteRenderer.sprite = weaponType.sprite;

        GameObject firingMechanismObject = Instantiate(weapon.firingMechanismPrefab, transform.position, Quaternion.identity);
        firingMechanismObject.transform.SetParent(transform);
        firingMechanismController = firingMechanismObject.GetComponent<FiringMechanismController>();

        weapon = weaponType;
    }
}
