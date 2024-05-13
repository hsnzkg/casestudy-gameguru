using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject]
    ILevelCreator _levelCreator;

    [Inject]
    LevelSettings _levelSetting;

    [Inject]
    IInputService _inputService;



    private void Awake()
    {
        _inputService.RegisterActionToPress(() => { Debug.Log("Space"); });
        _inputService.Activate();
        _levelCreator.CreateLevel(Vector2.zero, _levelSetting.levelDatas[DatabaseController.GetCurrentLevelIndex()]);
    }
}
