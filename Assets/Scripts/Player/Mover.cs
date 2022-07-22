using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requires a specific object to be automatically applied to a specific object type
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Mover : MonoBehaviour
{

    // Every mover has a rigidbody and a base movemnt speed
    public float baseMovementSpeed = 1f;
    public new Rigidbody2D rigidbody;

    // Defining a const for if no movement speed override is sent in
    private const float NO_MOVEMENT_OVERRIDE = -1;

    protected void move(Vector2 movementVector, float frameMovementSpeed=NO_MOVEMENT_OVERRIDE)
    {
        frameMovementSpeed = frameMovementSpeed == NO_MOVEMENT_OVERRIDE ? baseMovementSpeed : frameMovementSpeed;

        // Flip the sprite if moving the opposite way
        if (movementVector.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (movementVector.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Move the rigidbody
        rigidbody.velocity = Vector2.ClampMagnitude(movementVector, 1) * frameMovementSpeed * Time.fixedDeltaTime;
    }
}
