using UnityEngine;

/// <summary>
/// Controls the platform's dropping behavior
/// </summary>
public class DroppingPlatformScript : MonoBehaviour
{
    public float dropDelay;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerFeet"))
        {
            Invoke("StartDropping", dropDelay);
        }
    }

    void StartDropping()
    {
        rb.isKinematic = false;
    }
}
