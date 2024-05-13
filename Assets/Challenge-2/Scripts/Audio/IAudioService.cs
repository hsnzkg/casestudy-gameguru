using UnityEngine;

public interface IAudioService
{
    public void PlaySound(string clipName);
    public void IncreasePitch(string clipName);
    public void ResetPitch(string clipName);
}
