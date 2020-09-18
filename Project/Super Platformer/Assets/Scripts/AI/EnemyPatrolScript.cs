using System.Collections;
using UnityEngine;

/// <summary>
/// Provides a simple patrolling behavior between two positions
/// </summary>
public class EnemyPatrolScript : MonoBehaviour
{
    public Transform leftBound, rightBound;
    public float speed;
    public float maxDelay, minDelay;
    
    bool canTurn;
    float originalSpeed;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 

        SetStartingSpriteDirection();

        canTurn = true;
    }

    void Update()
    {
        Move();
        SetSpriteDirection();
    }

    void Move()
    {
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;
    }

    void SetSpriteDirection() 
    {
        if(spriteRenderer.flipX && transform.position.x >= rightBound.position.x)
        {
            if(canTurn)
            {
                canTurn = false;
                originalSpeed = speed;
                speed = 0;

                StartCoroutine("TurnLeft", originalSpeed);
            }
        }
        else if(!spriteRenderer.flipX && transform.position.x <= leftBound.position.x)
        {
            if(canTurn)
            {
                canTurn = false;
                originalSpeed = speed;
                speed = 0;

                StartCoroutine("TurnRight", originalSpeed);
            }
        }
    }

    void SetStartingSpriteDirection()
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

    IEnumerator TurnLeft(float originalSpeed)
    {
        animator.SetBool("isIdle", true);
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        animator.SetBool("isIdle", false);

        spriteRenderer.flipX = false;
        speed = -originalSpeed;
        canTurn = true;
    }

    IEnumerator TurnRight(float originalSpeed)
    {
        animator.SetBool("isIdle", true);
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        animator.SetBool("isIdle", false);

        spriteRenderer.flipX = true;
        speed = -originalSpeed;
        canTurn = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(leftBound.position, rightBound.position);
    }
}
