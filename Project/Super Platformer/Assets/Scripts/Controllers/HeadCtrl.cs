using UnityEngine;

public class HeadCtrl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Breakable"))
        {
            SFXCtrl.instance.HandleBoxBreaking(other.gameObject.transform.parent.transform.position);

            //set velocity of cat to 0
            gameObject.transform.parent.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
