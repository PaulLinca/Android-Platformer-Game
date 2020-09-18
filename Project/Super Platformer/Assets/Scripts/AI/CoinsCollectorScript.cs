using UnityEngine;

/// <summary>
/// This script "receives" the coins which fly towards it when the player picks them
/// </summary>
public class CoinsCollectorScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}
