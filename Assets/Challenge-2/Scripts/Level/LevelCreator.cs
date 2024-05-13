using UnityEngine;
using Zenject;

public class LevelCreator : ILevelCreator
{
    [Inject] private PrefabSettings _prefabSetting;
    [Inject] private BlockMovementController.Factory _blockMovementControllerFactory;


    public void CreateLevel(Vector3 startPos, LevelData data)
    {
        var emptyRootObject = new GameObject("Level Root : " + data.GetHashCode().ToString());
        CreateBlocks(startPos, emptyRootObject.transform,data);    
        CreateDroppingBlocks(emptyRootObject.transform,data);
        CreatePlayer();
    }

    private void CreateBlocks(Vector3 startPos,Transform root, LevelData data)
    {
        var iterationPosition = startPos;
        for (int i = 0; i < data.LevelBlockCount; i++)
        {
            GameObject go = _blockMovementControllerFactory.Create().gameObject;
            go.transform.parent = root;
            if (i != 0)
            {
                go.gameObject.SetActive(false);
            }
            go.gameObject.transform.position = iterationPosition;
            var offset = go.gameObject.transform.localScale.z * Vector3.forward;
            iterationPosition += offset;
        }
    }

    private void CreateDroppingBlocks(Transform root, LevelData data)
    {
        for (int i = 0; i < data.LevelDroppingBlockCount; i++)
        {
            GameObject go = Object.Instantiate(_prefabSetting.DroppingBlockPrefab, root);
            go.SetActive(false);

            var desiredPosition = Vector3.zero;
            go.transform.position = desiredPosition;
        }
    }

    private void CreatePlayer()
    {

    }
}