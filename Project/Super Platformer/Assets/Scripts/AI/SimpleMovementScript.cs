using UnityEngine;

/// <summary>
/// Makes the enemy move in a straight line
/// Also checks for collisions and reverses enemy movement direction
/// </summary>
public class SimpleMovementScript : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;

        SetSpriteDirection();
    }

    void SetSpriteDirection()
    {
        if(speed > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(speed < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void FlipDirection()
    {
        speed = -speed;
        SetSpriteDirection();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.gameObject.CompareTag("Player"))
        {
            FlipDirection();
        }
    }
}
