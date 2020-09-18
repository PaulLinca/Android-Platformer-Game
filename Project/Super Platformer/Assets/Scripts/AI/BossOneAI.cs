using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The AI Engine of Level One Boss
/// </summary>
public class BossOneAI : MonoBehaviour
{
    public float jumpSpeed;
    public int startJumpingAt;
    public int jumpDelay;
    public int health;
    public Slider bossHealth;
    public GameObject bossBullet;
    public float delayBeforeFiring;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Vector3 bulletSpawnPos;
    bool canFire, isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        bossHealth.value = (float) health;
        
        canFire = false;

        bulletSpawnPos = gameObject.transform.Find("BulletSpawnPosition").transform.position;

        Invoke("Reload", Random.Range(1f, delayBeforeFiring));
    }

    void Update()
    {
        if(canFire)
        {
            FireBullet();
            canFire = false;

            if(health < startJumpingAt && !isJumping)
            {
                InvokeRepeating("Jump", 0, jumpDelay);
                isJumping = true;
            }
        }
    }

    void Reload()
    {
        canFire = true;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpSpeed));
    }

    void FireBullet()
    {
        Instantiate(bossBullet, bulletSpawnPos, Quaternion.identity);
        Invoke("Reload", Random.Range(1f, delayBeforeFiring));
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            if(health == 0)
            {
                GameCtrl.instance.BulletHitEnemy(gameObject.transform);
            }

            if(health > 0)
            {
                health--;
                bossHealth.value = (float) health;

                sr.color = Color.red;
                Invoke("RestoreColor", 0.1f);
            }
        }
    }

    void RestoreColor()
    {
        sr.color = Color.white;
    }
}
