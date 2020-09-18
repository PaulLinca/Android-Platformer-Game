using UnityEngine;

/// <summary>
/// Checks when the player is stuck
/// </summary>
public class PlayerStuckScript : MonoBehaviour
{
    public GameObject player;

    PlayerCtrl playerCtrl;

    void Start()
    {
        playerCtrl = player.GetComponent<PlayerCtrl>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerCtrl.isStuck = true;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        playerCtrl.isStuck = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        playerCtrl.isStuck = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        playerCtrl.isStuck = false;
    }
}
