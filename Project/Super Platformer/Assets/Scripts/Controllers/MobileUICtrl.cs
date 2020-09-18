using UnityEngine;

/// <summary>
/// Routes the mobile input to the correct methods in the PlayerCtrl script
/// </summary>
public class MobileUICtrl : MonoBehaviour
{
    public GameObject player;

    PlayerCtrl playerCtrl;

    void Start()
    {
        playerCtrl = player.GetComponent<PlayerCtrl>();   
    }

    public void MobileMoveLeft()
    {
        playerCtrl.MobileMoveLeft();
    }
    
    public void MobileMoveRight()
    {
        playerCtrl.MobileMoveRight();
    }

    public void MobileStop()
    {
        playerCtrl.MobileStop();
    }

    public void MobileFire()
    {
        playerCtrl.MobileFire();
    }

    public void MobileJump()
    {
        playerCtrl.MobileJump();
    }
}
