using System.Collections.Generic;
using UnityEngine;

public interface ILevelCreator
{
    public void CreateLevel(Vector3 startPos,LevelData data);
}