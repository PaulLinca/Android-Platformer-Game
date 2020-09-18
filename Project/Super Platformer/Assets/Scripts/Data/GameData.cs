using System;

/// <summary>
/// The data model for the game data
/// </summary>
[Serializable]
public class GameData
{
    public int coinCount;
    public int score;
    public int lives;
    public bool[] keyFound;

    public bool playSound;
    public bool playMusic;

    public bool isFirstBoot;

    public LevelData[] levelData;
}
