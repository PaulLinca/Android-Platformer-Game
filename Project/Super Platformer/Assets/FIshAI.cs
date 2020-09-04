using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIshAI : MonoBehaviour
{
    public float jumpSpeed;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        FishJump();
    }

    void FishJump()
    {
        rb.AddForce(new Vector2(0, jumpSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y > 0)
        {
            spriteRenderer.flipY = false;
        }
        else
        {
            spriteRenderer.flipY = true;
        }
    }
}
