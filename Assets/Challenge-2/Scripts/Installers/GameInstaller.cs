using System.ComponentModel;
using UnityEngine;
using Zenject;
public class GameInstaller : MonoInstaller
{
    [Inject] private PrefabSettings prefabSettings;
    public override void InstallBindings()
    {
        Container.Bind<IAudioService>().To<AudioService>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<IInputService>().To<DesktopInputService>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<ILevelCreator>().To<LevelCreator>().AsSingle();
        Container.BindFactory<BlockMovementController, BlockMovementController.Factory>().FromComponentInNewPrefab(prefabSettings.BlockPrefab);
    }
}