using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class UI 
{
    [Header("Text")]
    public Text textCoinCount;
    public Text textScore;
    public Text textTimer;
    
    [Header("Images / Sprites")]
    public Image key0;
    public Image key1;
    public Image key2;
    public Sprite key0Full;
    public Sprite key1Full;
    public Sprite key2Full;

    public Image heart0;
    public Image heart1;
    public Image heart2;
    public Sprite heartFull;
    public Sprite heartEmpty;
    public Slider bossHealth;

    public GameObject panelGameOver;
}
