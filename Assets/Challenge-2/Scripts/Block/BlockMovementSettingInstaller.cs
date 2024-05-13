using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BlockMovementSettingInstaller", menuName = "Installers/BlockMovementSettingInstaller")]
public class BlockMovementSettingInstaller : ScriptableObjectInstaller<BlockMovementSettingInstaller>
{
    public BlockMovementSetting BlockMovementSetting;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<BlockMovementSetting>().FromInstance(BlockMovementSetting).AsSingle();
    }
}