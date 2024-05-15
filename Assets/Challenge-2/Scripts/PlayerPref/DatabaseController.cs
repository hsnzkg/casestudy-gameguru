using UnityEngine;


namespace Case_2
{
    public static class DatabaseController
    {
        public static int GetCurrentLevelIndex()
        {
            return PlayerPrefs.GetInt("LevelIndex", 0);
        }

        public static void SetLevelIndex(int index)
        {
            PlayerPrefs.SetInt("LevelIndex", index);
        }
    }
}
