using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelCreator : ILevelCreator
{
    [Inject] private PrefabSettings _prefabSetting;
    [Inject] private BlockMovementController.Factory _blockMovementControllerFactory;
    [Inject] private DroppingBlock.Factory _droppingBlockFactory;
    [Inject] private Player.Factory _playerFactory;


    private RuntimeLevelData _currentLevelData;
    public void CreateLevel(Vector3 startPos, LevelData data)
    {
        var emptyRootObject = new GameObject("Level Root : " + data.GetHashCode().ToString());
        var blocks = CreateBlocks(startPos, emptyRootObject.transform,data);
        var finishBlock = CreateFinishBlock(startPos, emptyRootObject.transform, data);
        var droppingBlocks = CreateDroppingBlocks(emptyRootObject.transform,data);
        var player = CreatePlayer(startPos);
        var runtimeLevelData = new RuntimeLevelData()
        {
            Blocks = blocks,
            DroppingBlocks = droppingBlocks,
            FinishBlock = finishBlock,
            Player = player,
        };
        _currentLevelData = runtimeLevelData;
    }

    public RuntimeLevelData GetRuntimeLevelData()
    {
        return _currentLevelData;
    }

    private List<BlockMovementController> CreateBlocks(Vector3 startPos,Transform root, LevelData data)
    {
        var blocks = new List<BlockMovementController>();   
        var iterationPosition = startPos;
        for (int i = 0; i < data.LevelBlockCount; i++)
        {
            BlockMovementController blockController = _blockMovementControllerFactory.Create();
            GameObject go = blockController.gameObject;
            go.transform.localScale = data.LevelBlockSize;
            blockController.SetInitialScale(data.LevelBlockSize);
            blocks.Add(blockController);

            go.transform.parent = root;
            if (i != 0)
            {
                go.gameObject.SetActive(false);
            }
            go.gameObject.transform.position = iterationPosition;
            var offset = go.gameObject.transform.localScale.z * Vector3.forward;
            iterationPosition += offset;
        }
        return blocks;
    }
    private GameObject CreateFinishBlock(Vector3 startPos, Transform root, LevelData data)
    {
        var finishBlock = Object.Instantiate(_prefabSetting.FinishBlockPrefab, root);
        var desiredPosition = startPos + (data.LevelBlockCount * data.LevelBlockSize.z * Vector3.forward) - (Vector3.forward * data.LevelBlockSize.z * 0.5f);
        finishBlock.transform.position = desiredPosition + (Vector3.forward * finishBlock.transform.localScale.z * 0.5f);
        var currentSize = finishBlock.transform.localScale;
        finishBlock.transform.localScale = new Vector3(currentSize.x, data.LevelBlockSize.y, currentSize.z);
        return finishBlock;
    }

    private List<DroppingBlock> CreateDroppingBlocks(Transform root, LevelData data)
    {
        var blocks = new List<DroppingBlock>();

        for (int i = 0; i < data.LevelDroppingBlockCount; i++)
        {
            DroppingBlock block = _droppingBlockFactory.Create();
            blocks.Add(block);
            var go = block.gameObject;
            go.SetActive(false);
            go.transform.parent = root;
        }
        return blocks;
    }

    private Player CreatePlayer(Vector3 startPos)
    {
        Player player = _playerFactory.Create();
        player.gameObject.transform.position = startPos;
        return player;
    }
}

public struct RuntimeLevelData
{
    public List<BlockMovementController> Blocks;
    public List<DroppingBlock> DroppingBlocks;
    public Player Player;
    public GameObject FinishBlock;

    public RuntimeLevelData(List<BlockMovementController> blocks,List<DroppingBlock> droppingBlocks,Player player,GameObject finishBlock)
    {
        Blocks = blocks;
        Player = player;
        FinishBlock = finishBlock;
        DroppingBlocks = droppingBlocks;
    }
}