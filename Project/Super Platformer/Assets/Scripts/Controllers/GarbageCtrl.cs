using UnityEngine;

/// <summary>
/// Destroys any GameObject that comes in contact with it
/// Restarts the level when Player connects
/// </summary>
public class GarbageCtrl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.PlayerDied(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
