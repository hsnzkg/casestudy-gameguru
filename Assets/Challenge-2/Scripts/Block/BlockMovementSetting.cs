using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings/BlockMovementSetting")]
[Serializable]
public class BlockMovementSetting : ScriptableObject
{
    public float BlockMovementDelta;
    public float SplitGoodThreshold;
    public float SplitBadThreshold;
}