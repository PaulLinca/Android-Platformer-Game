using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerCtrl : MonoBehaviour
{
    public Transform catTransform;
    public float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(catTransform.position.x, transform.position.y, transform.position.z);
        //transform.position = new Vector3(catTransform.position.x, catTransform.position.y + yOffset, transform.position.z);
    }
}
