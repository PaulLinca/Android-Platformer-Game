using UnityEngine;

public class FeetCtrl : MonoBehaviour
{
    public Transform dustParticlePosition;
    PlayerCtrl playerCtrl;
    
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
    }
}
