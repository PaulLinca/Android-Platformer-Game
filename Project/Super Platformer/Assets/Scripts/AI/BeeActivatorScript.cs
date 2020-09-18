using UnityEngine;

/// <summary>
/// Activates the Bomber Bee when the player comes near it
/// </summary>
public class BeeActivatorScript : MonoBehaviour
{
    public GameObject bee;
    
    BomberBeeAI beeAI;

    void Start()
    {
        beeAI = bee.GetComponent<BomberBeeAI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            beeAI.ActivateBee(other.gameObject.transform.position);
        }
    }
}
