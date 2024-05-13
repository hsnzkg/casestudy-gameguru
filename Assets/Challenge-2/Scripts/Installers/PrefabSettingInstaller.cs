using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PrefabSettingInstaller", menuName = "Installers/PrefabSettingInstaller")]
public class PrefabSettingInstaller : ScriptableObjectInstaller<PrefabSettingInstaller>
{
    public PrefabSettings PrefabSettings;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PrefabSettings>().FromInstance(PrefabSettings).AsSingle();
    }
}