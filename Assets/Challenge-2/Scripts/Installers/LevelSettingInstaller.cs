using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LevelSettingInstaller", menuName = "Installers/LevelSettingInstaller")]
public class LevelSettingInstaller : ScriptableObjectInstaller<LevelSettingInstaller>
{
    public LevelSettings LevelSettings;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<LevelSettings>().FromInstance(LevelSettings).AsSingle();
    }
}