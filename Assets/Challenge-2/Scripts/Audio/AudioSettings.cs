using UnityEngine;
using System;
using System.Collections.Generic;

namespace Case_2
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings/AudioSetting", order = 1)]
    [Serializable]
    public class AudioSettings : ScriptableObject
    {
        public float PitchIncreaseRate;
        public float PitchMaxRate;
        public int CachedAudioSourceCount;
        public List<ClipData> AudioClips;
    }

    [Serializable]
    public struct ClipData
    {
        public string ClipName;
        public AudioClip AudioClip;
    }

}
