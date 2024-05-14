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

    [Inject]
    BlockWaypoingController _blockWaypoingController;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _levelCreator.CreateLevel(Vector2.zero, _levelSetting.levelDatas[DatabaseController.GetCurrentLevelIndex()]);
        _inputService.RegisterActionToPress(StartGame);
        _inputService.Activate();   
    }

    private void StartGame()
    {
        _inputService.UnRegisterActionToPress(StartGame);
        _blockWaypoingController.ConfigureBlock();
        _levelCreator.GetRuntimeLevelData().Player.Activate();
    }
}
