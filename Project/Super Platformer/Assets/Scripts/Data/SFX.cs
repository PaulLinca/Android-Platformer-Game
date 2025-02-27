﻿using UnityEngine;
using System;

/// <summary>
/// Groups all the SFX together
/// </summary>
[Serializable]
public class SFX
{
    public GameObject sfx_coin_pickup; // Shown when player picks coins
    public GameObject sfx_bullet_pickup; // Shown when player picks bullet powerups
    public GameObject sfx_player_land; // Shown when player lands
    public GameObject sfx_box_fragment_1;
    public GameObject sfx_box_fragment_2; 
    public GameObject sfx_splash; // Shown when player falls in water
    public GameObject sfx_enemy_explosion;
}
