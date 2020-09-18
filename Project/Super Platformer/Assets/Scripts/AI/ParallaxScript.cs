using UnityEngine;

/// <summary>
/// Provides the Parallax Effect
/// </summary>
public class ParallaxScript : MonoBehaviour
{
    public float speed;
    float offsetX;
    Material mat;
    public GameObject player;
    PlayerCtrl playerCtrl;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        playerCtrl = player.GetComponent<PlayerCtrl>();
    }

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
