using UnityEngine;

/// <summary>
/// The AI Engine of the Jumping Fish
/// </summary>
public class FIshAI : MonoBehaviour
{
    public float jumpSpeed;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

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
