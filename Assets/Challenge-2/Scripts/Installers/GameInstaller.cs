using Zenject;
namespace Case_2
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] private PrefabSettings prefabSettings;
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<FXController>().FromComponentInHierarchy().AsSingle();

            Container.Bind<IAudioService>().To<AudioService>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<IInputService>().To<DesktopInputService>().FromNewComponentOnNewGameObject().AsSingle();

            Container.BindFactory<DroppingBlock, DroppingBlock.Factory>().FromComponentInNewPrefab(prefabSettings.DroppingBlockPrefab);
            Container.BindFactory<BlockMovementController, BlockMovementController.Factory>().FromComponentInNewPrefab(prefabSettings.BlockPrefab);
            Container.BindFactory<Player, Player.Factory>().FromComponentInNewPrefab(prefabSettings.CharacterPrefab);

            Container.Bind<ICameraController>().To<CMCameraController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ILevelCreator>().To<LevelCreator>().AsSingle();
            Container.Bind<BlockWaypointController>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}
