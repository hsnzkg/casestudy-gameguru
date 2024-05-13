using UnityEngine;

public static class DatabaseController
{
    public static int GetCurrentLevelIndex()
    {
        return PlayerPrefs.GetInt("LevelIndex", 0);
    }
}