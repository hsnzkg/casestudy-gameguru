using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerSettingInstaller", menuName = "Installers/PlayerSettingInstaller")]
public class PlayerSettingInstaller : ScriptableObjectInstaller<AudioSettingInstaller>
{
    public PlayerSettings PlayerSetting;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerSettings>().FromInstance(PlayerSetting).AsSingle();
    }
}