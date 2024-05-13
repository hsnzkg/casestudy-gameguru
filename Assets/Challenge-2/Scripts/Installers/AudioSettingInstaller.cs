using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AudioSettingInstaller", menuName = "Installers/AudioSettingInstaller")]
public class AudioSettingInstaller : ScriptableObjectInstaller<AudioSettingInstaller>
{
    public AudioSettings AudioSetting;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AudioSettings>().FromInstance(AudioSetting).AsSingle();
    }
}