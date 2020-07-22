using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Tooltip("This is a positive integer which speeds up the player movement.")]
    public int speedBoost = 5;
    [Tooltip("This is a positive integer which speeds up the player jump.")]
    public int jumpSpeed = 600;

    Rigidbody2D catRigidbody;
    SpriteRenderer catSpriteRenderer;

    void Start()
    {
        catRigidbody = GetComponent<Rigidbody2D>();
        catSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float playerSpeed = Input.GetAxisRaw("Horizontal"); //value will be 1, -1 or 0
        playerSpeed *= speedBoost;
        if (playerSpeed != 0)
        {
            MoveHorizontal(playerSpeed);
        }
        else
        {
            StopMoving();
        }

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void MoveHorizontal(float playerSpeed)
    {
        catRigidbody.velocity = new Vector2(playerSpeed, catRigidbody.velocity.y);

        if(playerSpeed < 0)
        {
            catSpriteRenderer.flipX = true;
        }
        else if(playerSpeed > 0)
        {
            catSpriteRenderer.flipX = false;
        }
    }

    void StopMoving()
    {
        catRigidbody.velocity = new Vector2(0, catRigidbody.velocity.y);
    }

    void Jump()
    {
        catRigidbody.AddForce(new Vector2(0, jumpSpeed));
    }
}