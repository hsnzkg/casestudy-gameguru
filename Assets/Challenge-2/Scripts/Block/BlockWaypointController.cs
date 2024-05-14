using UnityEngine;
using Zenject;

public class BlockWaypoingController : MonoInstaller
{
    [Inject]private ILevelCreator _levelCreator;

    public int CurrentWaypointIndex = 1;
    public Transform CurrentBlock;

    private int CurrentWaypointMovingIndex = 1;

    public void ConfigureBlock()
    {
        var runtimeData = _levelCreator.GetRuntimeLevelData();
        var previousIndex = Mathf.Clamp(CurrentWaypointMovingIndex - 1, 0, runtimeData.Blocks.Count - 2);

        var currentBlock = runtimeData.Blocks[CurrentWaypointMovingIndex];
        var previousBlock = runtimeData.Blocks[previousIndex];

        if (!currentBlock.ActivatedOnce)
        {
            currentBlock.Prepare(previousBlock.transform.position,previousBlock.transform.localScale);
            currentBlock.Activate();
            CurrentBlock = currentBlock.gameObject.transform;
        }
    }

    public void OnTargetReach()
    {
        var runtimeData = _levelCreator.GetRuntimeLevelData();
        var currentIndex = CurrentWaypointIndex + 1;
        if(currentIndex >= runtimeData.Blocks.Count)
        {
            CurrentBlock = runtimeData.FinishBlock.transform;
        }
        else
        {
            currentIndex = Mathf.Clamp(CurrentWaypointIndex + 1, 0, runtimeData.Blocks.Count - 1);
            CurrentWaypointIndex = currentIndex;
        }
    }

    public void OnPress()
    {
        var runtimeData = _levelCreator.GetRuntimeLevelData();
        var currentIndex = Mathf.Clamp(CurrentWaypointMovingIndex + 1, 0, runtimeData.Blocks.Count - 1);
        CurrentWaypointMovingIndex = currentIndex;
        ConfigureBlock();
    }
}