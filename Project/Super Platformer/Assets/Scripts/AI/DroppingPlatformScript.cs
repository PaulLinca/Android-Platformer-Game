using UnityEngine;

public class DroppingPlatformScript : MonoBehaviour
{
    Rigidbody2D rb;

    public float dropDelay;

    // Start is called before the first frame update
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
