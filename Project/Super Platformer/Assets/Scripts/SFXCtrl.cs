using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXCtrl : MonoBehaviour
{
    public static SFXCtrl instance;

    public GameObject sfx_coin_pickup;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ShowCoinSparkle(Vector3 position)
    {
        Instantiate(sfx_coin_pickup, position, Quaternion.identity);
    }
}
