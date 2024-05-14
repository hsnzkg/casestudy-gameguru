using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings/LevelSettings", order = 1)]
[Serializable]
public class LevelSettings : ScriptableObject
{
    public List<LevelData> levelDatas = new List<LevelData>();
}

[Serializable]
public struct LevelData
{
    public Vector3 LevelBlockSize;
    public int LevelBlockCount;
    public int LevelMaxBlockCount;
    public int LevelDroppingBlockCount;
}
