using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuckScript : MonoBehaviour
{
    public GameObject player;
    PlayerCtrl playerCtrl;

    // Start is called before the first frame update
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

    void OnTriggerExit2D(Collider2D other)
    {
        playerCtrl.isStuck = false;
    }
}
