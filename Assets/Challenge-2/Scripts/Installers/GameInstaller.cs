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

        Container.BindFactory<BlockMovementController, BlockMovementController.Factory>().FromComponentInNewPrefab(prefabSettings.BlockPrefab);
        Container.BindFactory<Player, Player.Factory>().FromComponentInNewPrefab(prefabSettings.CharacterPrefab);

        Container.Bind<ICameraController>().To<CMCameraController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ILevelCreator>().To<LevelCreator>().AsSingle();
        Container.Bind<BlockWaypoingController>().FromNewComponentOnNewGameObject().AsSingle();
    }
}