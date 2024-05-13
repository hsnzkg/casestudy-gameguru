using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AudioService : MonoBehaviour, IAudioService
{
    [Inject] AudioSettings settings;
    private Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        foreach(var clip in settings.AudioClips)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.clip = clip.AudioClip;
            sources.Add(clip.ClipName, source);
        }
    }

    public void PlaySound(string clipName)
    {
        var source = FindSource(clipName);
        if (source == null) return;
        source.Play();
    }

    public void IncreasePitch(string clipName)
    {
        var source = FindSource(clipName);
        if (source == null) return;
        var increaseAmount = settings.PitchIncreaseRate;
        var currentPitch = source.pitch;
        var desiredPitch = Mathf.Clamp(currentPitch + increaseAmount, 1f, settings.PitchMaxRate);
        source.pitch = desiredPitch;    
    }

    public void ResetPitch(string clipName)
    {
        var source = FindSource(clipName);
        if (source == null) return;
        source.pitch = 1f;
    }

    private AudioSource FindSource(string clipName)
    {
        foreach (var clip in sources)
        {
            if (clip.Key == clipName)
            {
                return clip.Value;
            }
        }
        return null;
    }
}