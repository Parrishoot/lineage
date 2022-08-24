using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requires a specific object to be automatically applied to a specific object type
[RequireComponent(typeof(Rigidbody2D))]

public class Mover : MonoBehaviour
{

    // Every mover has a rigidbody and a base movemnt speed
    public float baseMovementSpeed = 1f;
    public new Rigidbody2D rigidbody;

    public void Move(Vector2 movementVector)
    {
        Move(movementVector, baseMovementSpeed);
    }

    public void Move(Vector2 movementVector, float frameMovementSpeed)
    {
        // Move the rigidbody
        rigidbody.velocity = Vector2.ClampMagnitude(movementVector, 1) * frameMovementSpeed * Time.fixedDeltaTime;
    }
}
