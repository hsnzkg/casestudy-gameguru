using UnityEngine;
using System;

namespace Case_2
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings/PlayerSetting", order = 1)]
    [Serializable]
    public class PlayerSettings : ScriptableObject
    {
        public float AccelerationRate;
        public float DeAccelerationRate;
        public float MovementDelta;
        public float RotationDelta;
    }
}


