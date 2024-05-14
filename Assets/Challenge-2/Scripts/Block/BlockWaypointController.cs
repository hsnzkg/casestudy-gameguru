using System;
using UnityEngine;
using Zenject;

public class BlockWaypointController : MonoInstaller
{
    [Inject]private ILevelCreator _levelCreator;

    public int CurrentLevelBlockCount => _levelCreator.GetRuntimeLevelData().Blocks.Count;
    public int CurrentWaypointIndex = 1;
    public Transform CurrentBlock;
    private int CurrentWaypointMovingIndex = 1;

    public void Init()
    {
        var player = _levelCreator.GetRuntimeLevelData().Player;
        player.OnFall += OnFall;
    }

    private void OnFall()
    {
        var blocks = _levelCreator.GetRuntimeLevelData().Blocks;
        foreach (var b in blocks)
        {
            b.DeActivate();
        }
    }

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
            if(CurrentBlock == null) CurrentBlock = currentBlock.gameObject.transform;
        }
    }

    public void OnTargetReach()
    {
        var runtimeData = _levelCreator.GetRuntimeLevelData();
        var incrementedIndex = CurrentWaypointIndex + 1;
  
        if(incrementedIndex >= runtimeData.Blocks.Count)
        {
            CurrentBlock = runtimeData.FinishBlock.transform;
        }
        else
        {
            incrementedIndex = Mathf.Clamp(incrementedIndex, 0, runtimeData.Blocks.Count - 1);
            CurrentWaypointIndex = incrementedIndex;
            CurrentBlock = runtimeData.Blocks[incrementedIndex].transform;
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