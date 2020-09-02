using System;

[Serializable]
public class GameData
{
    public int coinCount;
    public int score;

    public bool[] keyFound;

    public int lives;
    public bool isFirstBoot;
}
