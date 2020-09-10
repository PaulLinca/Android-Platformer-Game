using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public GameObject dog;
    public Vector2 jumpSpeed;
    public GameObject[] stairs;
    public Sprite leverPulledSprite;
    
    Rigidbody2D rigidbodyDog;
    SpriteRenderer spriteRendererLever;

    void Start()
    {
        rigidbodyDog = dog.GetComponent<Rigidbody2D>();
        spriteRendererLever = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            spriteRendererLever.sprite = leverPulledSprite;
            
            rigidbodyDog.AddForce(jumpSpeed);

            foreach(GameObject stair in stairs)
            {
                stair.GetComponent<BoxCollider2D>().enabled = false;
            }

            SFXCtrl.instance.ShowPlayerLanding(gameObject.transform.position);
        }
    }
}
