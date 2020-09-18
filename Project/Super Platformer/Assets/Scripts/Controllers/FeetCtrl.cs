using UnityEngine;

/// <summary>
/// Shows the dust particle effects when the player lands
/// Handles jumping logic
/// </summary>
public class FeetCtrl : MonoBehaviour
{
    PlayerCtrl playerCtrl;

    public GameObject player;
    public Transform dustParticlePosition;

    void Start() 
    {
        playerCtrl = gameObject.transform.parent.gameObject.GetComponent<PlayerCtrl>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            SFXCtrl.instance.ShowPlayerLanding(dustParticlePosition.position);

            playerCtrl.isJumping = false;
        }

        if(other.gameObject.CompareTag("Platform"))
        {
            SFXCtrl.instance.ShowPlayerLanding(dustParticlePosition.position);

            playerCtrl.isJumping = false;

            player.transform.parent = other.gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            player.transform.parent = null;
        }
    }

}
