﻿using UnityEngine;

/// <summary>
/// Controls the enemy behavior when the player jumps on its head
/// </summary>
public class EnemyHeadCtrl : MonoBehaviour
{
    public GameObject enemy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerFeet"))
        {
            GameCtrl.instance.PlayerStompsEnemy(enemy);

            SFXCtrl.instance.ShowEnemyPoof(enemy.transform.position);

            AudioCtrl.instance.EnemyHit(enemy.transform.position);
        }
    }
}
