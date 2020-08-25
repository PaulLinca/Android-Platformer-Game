using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    public float speed;
    float offsetX;
    Material mat;
    public GameObject player;
    PlayerCtrl playerCtrl;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        playerCtrl = player.GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerCtrl.isStuck)
        {
            offsetX += Input.GetAxisRaw("Horizontal") * speed;

            if(playerCtrl.leftPressed)
            {
                offsetX += speed;
            }
            else if(playerCtrl.rightPressed)
            {
                offsetX += speed;
            }

            mat.SetTextureOffset("_MainTex", new Vector2(offsetX, 0));
        }
    }
}
