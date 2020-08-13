using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Tooltip("This is a positive integer which speeds up the player movement.")]
    public int speedBoost = 5;
    [Tooltip("This is a positive integer which speeds up the player jump.")]
    public int jumpSpeed = 600;

    bool leftPressed, rightPressed;
    bool isJumping;
    public bool isGrounded;
    public Transform feet;
    public float feetRadius;
    public LayerMask whatIsGround;
    public float boxWidth;
    public float boxHeight;
    public float delayForDoubleJump;
    bool canDoubleJump;
    public Transform leftBulletSpawnPos, rightBulletSpawnPos;
    public GameObject leftBullet, rightBullet;
    public bool SFXOn;
    public bool canFire;

    Rigidbody2D catRigidbody;
    SpriteRenderer catSpriteRenderer;
    Animator animator;

    void Start()
    {
        catRigidbody = GetComponent<Rigidbody2D>();
        catSpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(boxWidth, boxHeight), 360.0f, whatIsGround);

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

        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        ShowFalling();

        if (leftPressed)
        {
            MoveHorizontal(-speedBoost);
        }

        if (rightPressed)
        {
            MoveHorizontal(speedBoost);
        }
    }

    void FireBullet()
    {
        if(canFire)
        {
            if(catSpriteRenderer.flipX)
            {
                Instantiate(leftBullet, leftBulletSpawnPos.position, Quaternion.identity);
            }
            else
            {
                Instantiate(rightBullet, rightBulletSpawnPos.position, Quaternion.identity);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(feet.position, new Vector3(boxWidth, boxHeight, 0));
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

        if(!isJumping)
        {
            animator.SetInteger("State", 1);
        }
    }

    void StopMoving()
    {
        catRigidbody.velocity = new Vector2(0, catRigidbody.velocity.y);

        if (!isJumping)
        {
            animator.SetInteger("State", 0);
        }
    }

    public void MobileMoveLeft()
    {
        leftPressed = true;
    }
    public void MobileMoveRight()
    {
        rightPressed = true;

    }
    public void MobileStop()
    {
        leftPressed = false;
        rightPressed = false;

        StopMoving();
    }

    public void MobileFire()
    {
        FireBullet();
    }

    public void MobileJump()
    {
        Jump();
    }

    void Jump()
    {
        if(isGrounded)
        {
            catRigidbody.AddForce(new Vector2(0, jumpSpeed));

            isJumping = true;
            animator.SetInteger("State", 2);

            Invoke("EnableDoubleJump", delayForDoubleJump);
        }

        if(canDoubleJump && !isGrounded)
        {
            catRigidbody.velocity = Vector2.zero;
            catRigidbody.AddForce(new Vector2(0, jumpSpeed));
            animator.SetInteger("State", 2);

            canDoubleJump = false;
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    void ShowFalling()
    {
        if(catRigidbody.velocity.y < 0)
        {
            animator.SetInteger("State", 3);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        switch(other.gameObject.tag)
        {
            case "Coin":
                if(SFXOn)
                {
                    SFXCtrl.instance.ShowCoinSparkle(other.gameObject.transform.position);
                }
                break;
            case "Powerup_Bullet":
                canFire = true;
                if(SFXOn)
                {
                    SFXCtrl.instance.ShowBulletSparkle(other.gameObject.transform.position);
                }
                Destroy(other.gameObject);
                break;
            case "Water":
                SFXCtrl.instance.ShowWaterSplash(other.gameObject.transform.position);
                break;
            default:
                break;
        }
    }
}