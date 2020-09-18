using UnityEngine;

/// <summary>
/// Updates the HUD when a key is collected by the player
/// </summary>
public class KeyCtrl : MonoBehaviour
{
    public int keyNumber;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.UpdateKeyCount(keyNumber);

            SFXCtrl.instance.ShowKeySparkle(keyNumber);

            Destroy(gameObject);

            AudioCtrl.instance.KeyFound(gameObject.transform.position);
        }
    }
}
