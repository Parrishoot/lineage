using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperShotZoneController : MonoBehaviour
{

    private bool isColliding = false;

    public bool isSuccessfulCollision()
    {
        return isColliding;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("FiringMechanism"))
        {
            isColliding = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("FiringMechanism"))
        {
            isColliding = false;
        }
    }
}
