using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public GameObject dog;
    public Vector2 jumpSpeed;
    public GameObject[] stairs;

    Rigidbody2D rigidbodyDog;

    void Start()
    {
        rigidbodyDog = dog.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rigidbodyDog.AddForce(jumpSpeed);

            foreach(GameObject stair in stairs)
            {
                stair.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
