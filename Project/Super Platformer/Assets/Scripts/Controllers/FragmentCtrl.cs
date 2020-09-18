using UnityEngine;

/// <summary>
/// Manages the behavior of the box fragments
/// </summary>
public class FragmentCtrl : MonoBehaviour
{
    public Vector3 jumpForce;
    public float destroyDelay;
    
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(jumpForce);

        Destroy(gameObject, destroyDelay);
    }
}
