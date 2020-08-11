using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXCtrl : MonoBehaviour
{
    public static SFXCtrl instance;
    public SFX sfx;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ShowCoinSparkle(Vector3 position)
    {
        Instantiate(sfx.sfx_coin_pickup, position, Quaternion.identity);
    }

    public void ShowBulletSparkle(Vector3 position)
    {
        Instantiate(sfx.sfx_bullet_pickup, position, Quaternion.identity);
    }

    public void ShowPlayerLanding(Vector3 position)
    {
        Instantiate(sfx.sfx_player_land, position, Quaternion.identity);
    }
}
