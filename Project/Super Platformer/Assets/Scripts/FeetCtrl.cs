using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCtrl : MonoBehaviour
{
    public Transform dustParticlePosition;
    void OnTriggerEnter2D(Collider2D other) 
    {
        SFXCtrl.instance.ShowPlayerLanding(dustParticlePosition.position);
    }
}
