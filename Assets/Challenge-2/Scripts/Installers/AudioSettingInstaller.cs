using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AudioSettingInstaller", menuName = "Installers/AudioSettingInstaller")]
public class AudioSettingInstaller : ScriptableObjectInstaller<AudioSettingInstaller>
{
    public AudioSettings audioSetting;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AudioSettings>().FromInstance(audioSetting).AsSingle();
    }
}