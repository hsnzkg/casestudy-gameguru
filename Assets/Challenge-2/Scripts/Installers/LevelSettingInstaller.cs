using UnityEngine;
using Zenject;

namespace Case_2
{
    [CreateAssetMenu(fileName = "LevelSettingInstaller", menuName = "Installers/LevelSettingInstaller")]
    public class LevelSettingInstaller : ScriptableObjectInstaller<LevelSettingInstaller>
    {
        public LevelSettings LevelSettings;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelSettings>().FromInstance(LevelSettings).AsSingle();
        }
    }
}

