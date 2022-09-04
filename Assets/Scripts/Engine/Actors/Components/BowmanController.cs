using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class BowmanController : MonoBehaviour
{
    public PlayerBowController bowController;

    public Sprite regularSprite;
    public Sprite shootingSprite;

    public SpriteRenderer spriteRenderer;


    public void Update()
    {
        if(bowController.IsAiming())
        {
            spriteRenderer.sprite = shootingSprite;
        }
        else
        {
            spriteRenderer.sprite = regularSprite;
        }
    }
}
