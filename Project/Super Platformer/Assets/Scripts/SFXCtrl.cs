using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXCtrl : MonoBehaviour
{
    public static SFXCtrl instance;
    public SFX sfx;
    public Transform key0Pos, key1Pos, key2Pos;

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

    public void ShowKeySparkle(int keyNumber)
    {
        Vector3 position = Vector3.zero;
        if(keyNumber == 0)
        {
            position = key0Pos.position;
        }
        else if(keyNumber == 1)
        {
            position = key1Pos.position;
        }
        else if(keyNumber == 2)
        {
            position = key2Pos.position;
        }
        Instantiate(sfx.sfx_bullet_pickup, position, Quaternion.identity);
    }

    public void ShowBulletSparkle(Vector3 position)
    {
        Instantiate(sfx.sfx_bullet_pickup, position, Quaternion.identity);
    }

    public void ShowPlayerLanding(Vector3 position)
    {
        Instantiate(sfx.sfx_player_land, position, Quaternion.identity);
    }
    
    public void HandleBoxBreaking(Vector3 position)
    {
        Debug.Log("Cat head hit the crate");

        Vector3 pos1 = position;
        pos1.x -= 0.3f;
        Vector3 pos2 = position;
        pos1.x += 0.3f;
        Vector3 pos3 = position;
        pos1.x -= 0.3f;
        pos1.y -= 0.3f;
        Vector3 pos4 = position;
        pos1.x += 0.3f;
        pos1.y += 0.3f;

        Instantiate(sfx.sfx_box_fragment_1, pos1, Quaternion.identity);
        Instantiate(sfx.sfx_box_fragment_2, pos2, Quaternion.identity);
        Instantiate(sfx.sfx_box_fragment_2, pos3, Quaternion.identity);
        Instantiate(sfx.sfx_box_fragment_1, pos4, Quaternion.identity);
    }

    public void ShowWaterSplash(Vector3 position)
    {
        Instantiate(sfx.sfx_splash, position, Quaternion.identity);
    }

    public void ShowEnemyExplosion(Vector3 position)
    {
        Instantiate(sfx.sfx_enemy_explosion, position, Quaternion.identity);
    }
}
