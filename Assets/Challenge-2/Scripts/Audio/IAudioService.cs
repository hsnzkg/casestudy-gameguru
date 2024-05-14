using UnityEngine;

public interface IAudioService
{
    public void PlaySound(string clipName, float volumeLevel = 0.5f);
    public void IncreasePitch(string clipName);
    public void ResetPitch(string clipName);
    public AudioComboListener GetComboListener();
}
